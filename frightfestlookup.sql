SELECT	cs.ParkID,
	House = REPLACE(REPLACE(t.TranslationValue,'- Diamond Elite',''),'- Diamond',''),
	Total = COUNT(*),
	Last60min = SUM(CASE WHEN cs.Timestamp>DATEADD(HOUR,-1,GETDATE()) THEN 1 ELSE 0 END),
	t4pm = SUM(CASE WHEN DATEPART(HOUR,cs.Timestamp) =16 THEN 1 ELSE 0 END),
	t5pm = SUM(CASE WHEN DATEPART(HOUR,cs.Timestamp) =17 THEN 1 ELSE 0 END),
	t6pm = SUM(CASE WHEN DATEPART(HOUR,cs.Timestamp) =18 THEN 1 ELSE 0 END),
	t7pm = SUM(CASE WHEN DATEPART(HOUR,cs.Timestamp) =19 THEN 1 ELSE 0 END),
	t8pm = SUM(CASE WHEN DATEPART(HOUR,cs.Timestamp) =20 THEN 1 ELSE 0 END),
	t9pm = SUM(CASE WHEN DATEPART(HOUR,cs.Timestamp) =21 THEN 1 ELSE 0 END),
	t10pm = SUM(CASE WHEN DATEPART(HOUR,cs.Timestamp) =22 THEN 1 ELSE 0 END),
	t11pm = SUM(CASE WHEN DATEPART(HOUR,cs.Timestamp) =23 THEN 1 ELSE 0 END),
	t12am = SUM(CASE WHEN DATEPART(HOUR,cs.Timestamp) =0 THEN 1 ELSE 0 END),
	t1am = SUM(CASE WHEN DATEPART(HOUR,cs.Timestamp) =1 THEN 1 ELSE 0 END),
	t2am = SUM(CASE WHEN DATEPART(HOUR,cs.Timestamp) =1 THEN 1 ELSE 0 END),
	LastScan = FORMAT(MAX(Timestamp),'h:mm t')
FROM	Benefits.Coupon.CouponShred AS cs WITH (NOLOCK)
JOIN	Benefits.coupon.Coupon AS c WITH (NOLOCK) ON c.CouponCode = cs.CouponCodeAtTimeOfRedemption
JOIN	Benefits.Translation.ApplicationTranslation AS t WITH (NOLOCK) ON t.LookupId = c.CouponNameId
JOIN	(
	SELECT	cs.ParkID,
		Total = COUNT(*)
	FROM	Benefits.Coupon.CouponShred AS cs WITH (NOLOCK)
	JOIN	Benefits.coupon.Coupon AS c WITH (NOLOCK) ON c.CouponCode = cs.CouponCodeAtTimeOfRedemption
	JOIN	Benefits.Translation.ApplicationTranslation AS t WITH (NOLOCK) ON t.LookupId = c.CouponNameId
	WHERE	(CouponCodeAtTimeOfRedemption LIKE '2021FF%' or CouponCodeAtTimeOfRedemption LIKE '2022FF%')
	--AND	cs.Timestamp between DATEADD(HOUR,8,'9/20/2022') AND DATEADD(HOUR,26,'9/20/2022')
	AND	cs.Timestamp between DATEADD(HOUR,8,'{{date}}') AND DATEADD(HOUR,26,'{{date}}')
	GROUP BY	cs.ParkID
	HAVING	COUNT(*)>20
	) AS x ON x.ParkID = cs.ParkID
WHERE	(CouponCodeAtTimeOfRedemption LIKE '2021FF%' or CouponCodeAtTimeOfRedemption LIKE '2022FF%')
--AND	cs.Timestamp between DATEADD(HOUR,8,'9/20/2022') AND DATEADD(HOUR,26,'9/20/2022')
AND	cs.Timestamp between DATEADD(HOUR,8,'{{date}}') AND DATEADD(HOUR,26,'{{date}}')
GROUP BY	cs.ParkID,
	REPLACE(REPLACE(t.TranslationValue,'- Diamond Elite',''),'- Diamond','') WITH ROLLUP
ORDER BY	cs.ParkID,
	COUNT(*) DESC



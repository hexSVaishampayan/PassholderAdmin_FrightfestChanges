SELECT	ParkID,
	Guests = COUNT(*),
	DEGuests = SUM(CASE WHEN z.BandType=3 THEN 1 ELSE 0 END),
	DIGuests = SUM(CASE WHEN z.BandType=4 THEN 1 ELSE 0 END),
	SeasWGuests = SUM(CASE WHEN z.BandType=1 THEN 1 ELSE 0 END),
	DayWGuests = SUM(CASE WHEN z.BandType=2 THEN 1 ELSE 0 END),
	HouseVisits = SUM(visits),
	UniqueVisits = SUM(z.DiffHouses),
	DupeVisits = SUM(visits) - SUM(z.DiffHouses),
	AvgVisits = FORMAT(AVG(CONVERT(DECIMAL(12,4),visits)),'0.00'),
	AvgUniqueVisits = FORMAT(AVG(CONVERT(DECIMAL(12,4),z.DiffHouses)),'0.00'),
	Vis1Only = SUM(CASE WHEN z.VisTwice = 0 and z.VisThree = 0 THEN 1 ELSE 0 end),
	Vis2Plus = SUM(CASE WHEN z.VisTwice > 0 OR z.VisThree > 0 THEN 1 ELSE 0 end),
	DEVisits = SUM(CASE WHEN z.BandType=3 THEN z.Visits ELSE 0 END),
	DIVisits = SUM(CASE WHEN z.BandType=4 THEN z.Visits ELSE 0 END),
	SeasWVisits = SUM(CASE WHEN z.BandType=1 THEN z.Visits ELSE 0 END),
	DayWVisits = SUM(CASE WHEN z.BandType=2 THEN z.Visits ELSE 0 END),
	DEAvg = CASE WHEN SUM(CASE WHEN z.BandType=3 THEN CONVERT(DECIMAL(12,4),1) ELSE 0 END) > 0 THEN FORMAT(SUM(CASE WHEN z.BandType=3 THEN z.DiffHouses ELSE 0 END)/SUM(CASE WHEN z.BandType=3 THEN CONVERT(DECIMAL(12,4),1) ELSE 0 END),'0.00') ELSE '0.00' END,
	DIAvg = CASE WHEN SUM(CASE WHEN z.BandType=4 THEN CONVERT(DECIMAL(12,4),1) ELSE 0 END) > 0 THEN FORMAT(SUM(CASE WHEN z.BandType=4 THEN z.DiffHouses ELSE 0 END)/SUM(CASE WHEN z.BandType=4 THEN CONVERT(DECIMAL(12,4),1) ELSE 0 END),'0.00') ELSE '0.00' end,
	SeasWAvg = CASE WHEN SUM(CASE WHEN z.BandType=1 THEN CONVERT(DECIMAL(12,4),1) ELSE 0 END) > 0 THEN FORMAT(SUM(CASE WHEN z.BandType=1 THEN z.DiffHouses ELSE 0 END)/SUM(CASE WHEN z.BandType=1 THEN CONVERT(DECIMAL(12,4),1) ELSE 0 END),'0.00') ELSE '0.00' end,
	DayWAvg = CASE WHEN SUM(CASE WHEN z.BandType=2 THEN CONVERT(DECIMAL(12,4),1) ELSE 0 END) > 0 THEN FORMAT(SUM(CASE WHEN z.BandType=2 THEN z.DiffHouses ELSE 0 END)/SUM(CASE WHEN z.BandType=2 THEN CONVERT(DECIMAL(12,4),1) ELSE 0 END),'0.00') ELSE '0.00' end,
	HVisits01 = SUM(CASE WHEN Visits=1 THEN 1 ELSE 0 END),
	HVisits02 = SUM(CASE WHEN Visits=2 THEN 1 ELSE 0 END),
	HVisits03 = SUM(CASE WHEN Visits=3 THEN 1 ELSE 0 END),
	HVisits04 = SUM(CASE WHEN Visits=4 THEN 1 ELSE 0 END),
	HVisits05 = SUM(CASE WHEN Visits=5 THEN 1 ELSE 0 END),
	HVisits06 = SUM(CASE WHEN Visits=6 THEN 1 ELSE 0 END),
	HVisits07 = SUM(CASE WHEN Visits=7 THEN 1 ELSE 0 END),
	HVisits08 = SUM(CASE WHEN Visits=8 THEN 1 ELSE 0 END),
	HVisits09 = SUM(CASE WHEN Visits=9 THEN 1 ELSE 0 END),
	HVisits10 = SUM(CASE WHEN Visits=10 THEN 1 ELSE 0 END),
	HVisits11 = SUM(CASE WHEN Visits=11 THEN 1 ELSE 0 END),
	HVisits12 = SUM(CASE WHEN Visits=12 THEN 1 ELSE 0 END),
	HVisits13 = SUM(CASE WHEN Visits=13 THEN 1 ELSE 0 END),
	HVisits14 = SUM(CASE WHEN Visits=14 THEN 1 ELSE 0 END),
	HVisits15 = SUM(CASE WHEN Visits=15 THEN 1 ELSE 0 END),
	HVisits16 = SUM(CASE WHEN Visits=16 THEN 1 ELSE 0 END),
	HVisits17 = SUM(CASE WHEN Visits=17 THEN 1 ELSE 0 END),
	HVisits18 = SUM(CASE WHEN Visits=18 THEN 1 ELSE 0 END),
	HVisits19 = SUM(CASE WHEN Visits=19 THEN 1 ELSE 0 END),
	HVisits20 = SUM(CASE WHEN Visits=20 THEN 1 ELSE 0 END)
FROM	(
	SELECT	y.FolioID,
		ParkID = MAX(y.ParkID),
		FirstScan = MAX(y.FirstScan),
		LastScan = MAX(y.LastScan),
		Visits = MAX(y.Visits),
		DiffHouses = MAX(y.DiffHouses),
		VisOnce = MAX(y.VisOnce),
		VisTwice = MAX(y.VisTwice),
		VisThree = MAX(y.VisThree),
		PassType = MAX(CASE WHEN r.ProductTypeID IN (15,16,53,57,61,2,3) AND r.VoidDate IS null THEN r.ProductTypeID ELSE 0 end),
		PassLevel = MAX(CASE WHEN r.ProductTypeID IN (15,16,53,57,61,2,3) AND r.VoidDate IS null THEN r.ProductLevelID ELSE 0 end),
		BandType = min(CASE WHEN r.ProductTypeID IN (26) THEN 1 
			        WHEN r.ProductTypeID IN (27) THEN 2
			        WHEN r.ProductTypeID IN (53,57) AND r.VoidDate IS null AND r.ProductLevelID IN (14,17) THEN 3
			        WHEN r.ProductTypeID IN (53,57) AND r.voiddate IS NULL AND r.ProductLevelID IN (13) THEN 4
		ELSE null END)
	FROM	(
		SELECT	x.FolioID,
			Parkid = MAX(ParkID),
			FirstScan = min(FirstScan),
			LastScan = MAX(LAstScan),
			Visits = SUM(visits),
			DiffHouses = COUNT(*),
			VisOnce = SUM(CASE WHEN visits=1 THEN 1 ELSE 0 END),
			VisTwice = SUM(CASE WHEN visits=2 THEN 1 ELSE 0 END),
			VisThree = SUM(CASE WHEN visits>=3 THEN 1 ELSE 0 END)
		FROM	(
			SELECT	cs.ParkID,
				cs.FolioID,
				CouponCode = cs.CouponCodeAtTimeOfRedemption,
				FirstScan = MIN(cs.Timestamp),
				LastScan = MIN(cs.Timestamp),
				Visits = COUNT(*)
			FROM	Benefits.Coupon.CouponShred AS cs WITH (NOLOCK)
			WHERE	(CouponCodeAtTimeOfRedemption LIKE '2021FF%' or CouponCodeAtTimeOfRedemption LIKE '2022FF%')
			AND	cs.Timestamp between DATEADD(HOUR,8,'{{date}}') AND DATEADD(HOUR,26,'{{date}}')
			--AND	cs.Timestamp between DATEADD(HOUR,8,'9/20/2022') AND DATEADD(HOUR,26,'9/20/2022')
			GROUP BY	cs.ParkID,
				cs.FolioID,
				cs.CouponCodeAtTimeOfRedemption
			) AS x
		GROUP BY	x.FolioID
		) AS y
	LEFT OUTER JOIN Ticketing.dbo.Redeemable AS r WITH (NOLOCK) ON r.FolioID = y.FolioID AND r.VoidDate IS null
	GROUP BY	y.FolioID
	) AS z
GROUP BY	z.ParkID



CREATE VIEW [dbo].[VWHDBMASTER]
	AS 
	SELECT a.*
		,DATEADD(DAY,-1,DATEADD(YEAR, a.InsuranceTerm, a.InsuranceEffectiveDate)) as InsuranceExpiryDate
		,DATEADD(YEAR, a.InsuranceTerm, a.InsuranceEffectiveDate) as RenewalEffectiveDate
		,b.HDBFlatType
	FROM dbo.HDBMASTER a
		LEFT JOIN HDBFLATTYPES b on b.FlatTypeClassification = a.FlatTypeClassification
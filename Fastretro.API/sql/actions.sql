truncate table [anowakowski_retrodb].[anowakowski_retrousr].[RetroBoardActionCards]


SELECT TOP (1000) [Id]
      ,[RetroBoardFirebaseDocId]
      ,[RetroBoardCardFirebaseDocId]
      ,[RetroBoardActionCardFirebaseDocId]
      ,[Text]
  FROM [anowakowski_retrodb].[anowakowski_retrousr].[RetroBoardActionCards]
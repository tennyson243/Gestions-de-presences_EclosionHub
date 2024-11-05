SELECT
    I.Id AS IncubeId,
    I.Nom AS IncubeNom,
    M.Id AS ModuleId,
    M.Nom AS ModuleNom,
    CASE
        WHEN DATEDIFF(DAY, M.Date_debut, M.Date_fin) >= 1 THEN DATEDIFF(DAY, M.Date_debut, M.Date_fin) + 1
        ELSE 1
    END AS TotalSessionsModule,
    SUM(CASE WHEN P.Present = 'Oui' THEN 1 ELSE 0 END) AS SessionsPresentes,
    SUM(CASE WHEN P.Present = 'Non' THEN 1 ELSE 0 END) AS SessionsAbsent,
    CASE
        WHEN COUNT(P.Id) > 0 THEN (SUM(CASE WHEN P.Present = 'Oui' THEN 1 ELSE 0 END) * 100.0 / COUNT(P.Id))
        ELSE 0
    END AS PourcentagePresenceIncube
FROM
    Incubes I
    CROSS JOIN Modules M
    LEFT JOIN Presence P ON I.Id = P.Incube AND M.Id = P.Module
WHERE
    I.Id = 3 AND M.Id = 1
GROUP BY
    I.Id, I.Nom, M.Id, M.Nom, M.Date_debut, M.Date_fin, P.Date_Scan;


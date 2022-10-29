using ConsoleLab;
using FeirasLivres.Domain.Entities.FeiraEntity;
using FeirasLivres.Infrastructure.Data.DbCtx;
using Microsoft.EntityFrameworkCore;

// Defalt CSV source file » ./[ThisVisualStudioSolution]/SqliteDb/DEINFO_AB_FEIRASLIVRES_2014.csv
var csvFilePath = getCsvFilePath(); // Change here to target another CSV source file

// Defalt SQLite data base file » ./[ThisVisualStudioSolution]/SqliteDb/feiras-livres.csv
var sqliteDbFilePath = getSqliteDbFilePath(); // Change here to target another SQLite data base file

var ctxDbOpts = new DbContextOptionsBuilder<FeirasLivresDbContext>()
    .UseSqlite(@$"Data Source={sqliteDbFilePath}")
    .Options;

var sqLiteEFDbContext = new FeirasLivresDbContext(ctxDbOpts);

var importedFeiras = new ImportCsvData(sqLiteEFDbContext, csvFilePath).Execute();

showImportedFeiras(importedFeiras);






void showImportedFeiras(List<Feira> importedFeiras)
{
    Console.Clear();
    importedFeiras.OrderBy(f => f.Nome).ToList().ForEach(x => Console.WriteLine($"{x.Nome} ({x.NumeroRegistro})"));
    Console.WriteLine($"\n\nTOTAL DE FEIRAS IMPORTADAS: {importedFeiras.Count}\n\n");
    Console.ReadKey();
}

string getSqliteDbFilePath()
{
    var currentPath = Directory.GetCurrentDirectory();
    var csvFilePath = currentPath.Substring(0, currentPath.IndexOf(@"ConsoleLab\")) + @"SqliteDb\feiras-livres.db";
    return csvFilePath;
}

string getCsvFilePath()
{
    var currentPath = Directory.GetCurrentDirectory();
    var csvFilePath = currentPath.Substring(0, currentPath.IndexOf(@"ConsoleLab\")) + @"SqliteDb\DEINFO_AB_FEIRASLIVRES_2014.csv";
    return csvFilePath;
}
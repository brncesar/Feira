using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Domain.Entities.Enums;
using FeirasLivres.Domain.Entities.FeiraEntity;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity;
using FeirasLivres.Infrastructure.Data.DbCtx;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;

var ctxDbOpts  = new DbContextOptionsBuilder<FeirasLivresDbContext>().UseSqlite(@$"Data Source={getSqliteDbFilePath()}").Options;
var ctx        = new FeirasLivresDbContext(ctxDbOpts);

var linhasCsv      = GetLinhasCsv();
var feiras         = new List<Feira        >();
var distritos      = new List<Distrito     >();
var subPrefeituras = new List<SubPrefeitura>();

clearAllTables();
foreach (var linhaCsv in linhasCsv)
{
    AddDistrito(linhaCsv);
    AddSubPrefeitura(linhaCsv);
    AddFeira(linhaCsv);
}
ctx.SaveChanges();

Console.WriteLine("Distritos:");
distritos.ForEach(x => Console.WriteLine($"\t({x.Codigo}) {x.Nome}"));

Console.WriteLine("\n\nSubPrefeituras:");
subPrefeituras.ForEach(x => Console.WriteLine($"\t({x.Codigo}) {x.Nome}"));

Console.WriteLine("\n\nFeiras da zona Centro:");
feiras.Where(x => x.Regiao5 == Regiao5.Centro).ToList()
    .ForEach(x => Console.WriteLine($"\t({x.NumeroRegistro}) {x.Nome}"));


void clearAllTables()
{
    ctx.Database.ExecuteSqlRaw("DELETE FROM [TP01_Feira]"        );
    ctx.Database.ExecuteSqlRaw("DELETE FROM [TS01_Distrito]"     );
    ctx.Database.ExecuteSqlRaw("DELETE FROM [TS02_SubPrefeitura]");
}

void AddDistrito(LinhaCsv linhaCsv) {
    if (distritos.Any(x => x.Codigo == linhaCsv.CODDIST)) return;

    distritos.Add(new Distrito{
        Codigo = linhaCsv.CODDIST,
        Nome   = linhaCsv.DISTRITO,
    });

    ctx.Set<Distrito>().Add(distritos.Last());
}

void AddSubPrefeitura(LinhaCsv linhaCsv)
{
    if (subPrefeituras.Any(x => x.Codigo == linhaCsv.CODSUBPREF)) return;

    subPrefeituras.Add(new SubPrefeitura
    {
        Codigo = linhaCsv.CODSUBPREF,
        Nome   = linhaCsv.SUBPREFE,
    });

    ctx.Set<SubPrefeitura>().Add(subPrefeituras.Last());
}

void AddFeira(LinhaCsv linhaCsv)
{
    feiras.Add(new Feira
    {
        Nome                 = linhaCsv.NOME_FEIRA,
        NumeroRegistro       = linhaCsv.REGISTRO,
        SetorCensitarioIBGE  = linhaCsv.SETCENS,
        AreaDePonderacaoIBGE = linhaCsv.AREAP,
        DistritoId           = GetIdDistrito(linhaCsv.CODDIST),
        SubPrefeituraId      = GetIdSubPrefeitura(linhaCsv.CODSUBPREF),
        Regiao5              = GetRegiao5(linhaCsv.REGIAO5),
        Regiao8              = GetRegiao8(linhaCsv.REGIAO8),
        EnderecoLogradouro   = linhaCsv.LOGRADOURO,
        EnderecoNumero       = linhaCsv.NUMERO,
        EnderecoBairro       = linhaCsv.BAIRRO,
        EnderecoReferencia   = linhaCsv.REFERENCIA,
        Latitude             = GetCoordenada(linhaCsv.LAT),
        Longitude            = GetCoordenada(linhaCsv.LONG),
    });

    ctx.Set<Feira>().Add(feiras.Last());
}

Guid GetIdDistrito     (string cod) => distritos     .First(x => x.Codigo == cod).Id;
Guid GetIdSubPrefeitura(string cod) => subPrefeituras.First(x => x.Codigo == cod).Id;

double GetCoordenada(string coord) => Convert.ToDouble($"{coord.Substring(0, 3)}.{coord.Substring(3)}");

Regiao5 GetRegiao5(string regiao5) => regiao5 switch {
    "Norte"  => Regiao5.Norte,
    "Leste"  => Regiao5.Leste,
    "Sul"    => Regiao5.Sul,
    "Oeste"  => Regiao5.Oeste,
    "Centro" => Regiao5.Centro,
    _        => Regiao5.Norte,
};

Regiao8 GetRegiao8(string regiao8) => regiao8 switch {
    "Norte1" => Regiao8.Norte1,
    "Norte2" => Regiao8.Norte2,
    "Leste1" => Regiao8.Leste1,
    "Leste2" => Regiao8.Leste2,
    "Sul1"   => Regiao8.Sul1,
    "Sul2"   => Regiao8.Sul2,
    "Oeste"  => Regiao8.Oeste,
    "Centro" => Regiao8.Centro,
    _        => Regiao8.Norte1,
};

List<LinhaCsv> GetLinhasCsv(){
    List<LinhaCsv> linhasCsv = new List<LinhaCsv>();
    using (TextFieldParser parser = new TextFieldParser(getCsvFilePath()))
    {
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");
        while (!parser.EndOfData)
            linhasCsv.Add(GetNewLinhaCsvFromCsvRow(parser.ReadFields()));
    }
    linhasCsv.RemoveAt(0);

    return linhasCsv;
}

string getCsvFilePath()
{
    var currentPath = Directory.GetCurrentDirectory();
    var csvFilePath = currentPath.Substring(0, currentPath.IndexOf(@"ConsoleLab\")) + @"SqliteDb\DEINFO_AB_FEIRASLIVRES_2014.csv";
    return csvFilePath;
}

string getSqliteDbFilePath()
{
    var currentPath = Directory.GetCurrentDirectory();
    var csvFilePath = currentPath.Substring(0, currentPath.IndexOf(@"ConsoleLab\")) + @"SqliteDb\feiras-livres.db";
    return csvFilePath;
}

LinhaCsv GetNewLinhaCsvFromCsvRow(string[] fields)
{
    return new(
        fields[ 0], fields[ 1], fields[ 2], fields[ 3], fields[ 4], fields[ 5],
        fields[ 6], fields[ 7], fields[ 8], fields[ 9], fields[10], fields[11],
        fields[12], fields[13], fields[14], fields[15],
        fields.Length == 17  ?  fields[16] : "");
}

record LinhaCsv(
    string ID,
    string LONG,
    string LAT,
    string SETCENS,
    string AREAP,
    string CODDIST,
    string DISTRITO,
    string CODSUBPREF,
    string SUBPREFE,
    string REGIAO5,
    string REGIAO8,
    string NOME_FEIRA,
    string REGISTRO,
    string LOGRADOURO,
    string NUMERO,
    string BAIRRO,
    string REFERENCIA);
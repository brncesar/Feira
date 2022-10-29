using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Domain.Entities.Enums;
using FeirasLivres.Domain.Entities.FeiraEntity;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity;
using FeirasLivres.Infrastructure.Data.DbCtx;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;

namespace ConsoleLab
{
    internal class ImportCsvData
    {
        private string? _csvFilePath;
        private readonly FeirasLivresDbContext _ctx;
        private readonly List<Feira>           _feiras         = new List<Feira        >();
        private readonly List<Distrito>        _distritos      = new List<Distrito     >();
        private readonly List<SubPrefeitura>   _subPrefeituras = new List<SubPrefeitura>();

        public ImportCsvData(FeirasLivresDbContext ctx, string? csvFilePath = null)
            => (_ctx, _csvFilePath) = (ctx, csvFilePath);

        public List<Feira> Execute()
        {
            clearAllTables();
            foreach (var csvLine in GetCsvLines(_csvFilePath))
            {
                AddDistrito     (csvLine);
                AddSubPrefeitura(csvLine);
                AddFeira        (csvLine);
            }
            _ctx.SaveChanges();

            return _feiras;
        }

        void clearAllTables()
        {
            _ctx.Database.ExecuteSqlRaw("DELETE FROM [TP01_Feira]        ");
            _ctx.Database.ExecuteSqlRaw("DELETE FROM [TS01_Distrito]     ");
            _ctx.Database.ExecuteSqlRaw("DELETE FROM [TS02_SubPrefeitura]");
        }

        void AddDistrito(LinhaCsv linhaCsv)
        {
            if (_distritos.Any(x => x.Codigo == linhaCsv.CODDIST)) return;

            _distritos.Add(new Distrito
            {
                Codigo = linhaCsv.CODDIST,
                Nome   = linhaCsv.DISTRITO,
            });

            _ctx.Set<Distrito>().Add(_distritos.Last());
        }

        void AddSubPrefeitura(LinhaCsv linhaCsv)
        {
            if (_subPrefeituras.Any(x => x.Codigo == linhaCsv.CODSUBPREF)) return;

            _subPrefeituras.Add(new SubPrefeitura
            {
                Codigo = linhaCsv.CODSUBPREF,
                Nome   = linhaCsv.SUBPREFE,
            });

            _ctx.Set<SubPrefeitura>().Add(_subPrefeituras.Last());
        }

        void AddFeira(LinhaCsv linhaCsv)
        {
            Distrito      GetDistrito     (string cod) => _distritos     .First(x => x.Codigo == cod);
            SubPrefeitura GetSubPrefeitura(string cod) => _subPrefeituras.First(x => x.Codigo == cod);

            _feiras.Add(new Feira
            {
                Nome                 = linhaCsv.NOME_FEIRA,
                NumeroRegistro       = linhaCsv.REGISTRO,
                SetorCensitarioIBGE  = linhaCsv.SETCENS,
                AreaDePonderacaoIBGE = linhaCsv.AREAP,
                Distrito             = GetDistrito(linhaCsv.CODDIST),
                DistritoId           = GetDistrito(linhaCsv.CODDIST).Id,
                SubPrefeitura        = GetSubPrefeitura(linhaCsv.CODSUBPREF),
                SubPrefeituraId      = GetSubPrefeitura(linhaCsv.CODSUBPREF).Id,
                Regiao5              = GetRegiao5(linhaCsv.REGIAO5),
                Regiao8              = GetRegiao8(linhaCsv.REGIAO8),
                EnderecoLogradouro   = linhaCsv.LOGRADOURO,
                EnderecoNumero       = linhaCsv.NUMERO,
                EnderecoBairro       = linhaCsv.BAIRRO,
                EnderecoReferencia   = linhaCsv.REFERENCIA,
                Latitude             = GetCoordenada(linhaCsv.LAT),
                Longitude            = GetCoordenada(linhaCsv.LONG),
            });

            _ctx.Set<Feira>().Add(_feiras.Last());
        }

        double GetCoordenada(string coord) => Convert.ToDouble($"{coord.Substring(0, 3)}.{coord.Substring(3)}", System.Globalization.CultureInfo.InvariantCulture);

        Regiao5 GetRegiao5(string regiao5) => regiao5 switch
        {
            "Norte"  => Regiao5.Norte,
            "Leste"  => Regiao5.Leste,
            "Sul"    => Regiao5.Sul,
            "Oeste"  => Regiao5.Oeste,
            "Centro" => Regiao5.Centro,
            _        => Regiao5.Norte,
        };

        Regiao8 GetRegiao8(string regiao8) => regiao8 switch
        {
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

        List<LinhaCsv> GetCsvLines(string? strCsvFilePath = null)
        {
            List<LinhaCsv> linhasCsv = new List<LinhaCsv>();
            using (TextFieldParser parser = new TextFieldParser(strCsvFilePath ?? getCsvFilePath()))
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

        LinhaCsv GetNewLinhaCsvFromCsvRow(string[] fields)
        {
            return new(
                fields[ 0], fields[ 1], fields[ 2], fields[ 3], fields[ 4], fields[ 5],
                fields[ 6], fields[ 7], fields[ 8], fields[ 9], fields[10], fields[11],
                fields[12], fields[13], fields[14], fields[15],
                fields.Length == 17 ? fields[16] : "");
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
    }
}

using FeirasLivres.Domain.Entities.Enums;
using ConsoleLab;
using FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase;
using FeirasLivres.Infrastructure.FakeInMemory.Data;
using FeirasLivres.Domain.Entities.Common;

var feiraRepository         = new FeiraRepositoryMemory();
var distritoRepository      = new DistritoRepositoryMemory();
var subPrefeituraRepository = new SubPrefeituraRepositoryMemory();

var addNewFeiraParams = new AddNewFeiraParams(
    Nome                 : "PIRASSUNUNGA",
    NumeroRegistro       : "0000-0",
    SetorCensitarioIBGE  : "355030801000054",
    AreaDePonderacaoIBGE : "3550308005039",
    Regiao5              : Regiao5.Leste,
    Regiao8              : Regiao8.Leste1,
    EnderecoLogradouro   : "RUA TEREZINA",
    EnderecoNumero       : "615",
    EnderecoBairro       : "ALTO DA MOOCA",
    EnderecoReferencia   : "CAMPO LARGO E MANAUS",
    Latitude             : 0,
    Longitude            : 0,
    DistritoId           : new Guid("9a3a04aa-5069-4ec6-86ff-7572b24e8f22"),
    SubPrefeituraId      : new Guid("2335194b-95e3-4ce1-9bc8-f9cc3c09943d")
);

var addNewFeiraUseCase = new AddNewFeira(feiraRepository, distritoRepository, subPrefeituraRepository);

var addNewFeiraResult = AsyncHelper.RunSync(() => addNewFeiraUseCase.Execute(addNewFeiraParams));

if (addNewFeiraResult.HasErrors())
    addNewFeiraResult.Errors.ToList().ForEach( err => Console.WriteLine(err) );
else
    Console.WriteLine($"Id nova feira cadastrada: {addNewFeiraResult.Value?.Id}");

Console.ReadKey();


/*
// Execute the validator
ValidationResult results = new AddNewFeiraParamsValidator().Validate(addNewFeiraParams);

// Inspect any validation failures.
bool success = results.IsValid;
List<ValidationFailure> failures = results.Errors;


Feira f = addNewFeiraParams.CopyValuesTo<Feira>();
*/

//async Task<DomainServiceResult<AddNewFeiraResult>> AddNovaFeira(AddNewFeiraParams newFeiraInfos)
//{
//    var _feiraRepository = new FeiraRepositoryMemory();

//    var paramsValidationResult = new AddNewFeiraParamsValidator().Validate(newFeiraInfos);
//    var AddNewFeiraResult = new DomainServiceResult<AddNewFeiraResult>(paramsValidationResult.Errors);

//    var strNamePropNumeroRegistro = nameof(newFeiraInfos.NumeroRegistro);
//    var isParamNumeroRegistroValid = paramsValidationResult.IsParamValid(strNamePropNumeroRegistro);
//    if (isParamNumeroRegistroValid)
//    {
//        var getFeiraRepositoryResult = await _feiraRepository.GetByNumeroRegistroAsync(newFeiraInfos.NumeroRegistro);

//        if (getFeiraRepositoryResult.IsSuccess())
//        {
//            var existingFeiraReceivedFromRepository = getFeiraRepositoryResult.Value;

//            if (existingFeiraReceivedFromRepository is not null)
//                AddNewFeiraResult.AddError(AddNewFeiraErrors.DuplicateFeira(
//                    $"Já existe uma outra feira cadastrada com este mesmo número de registro: " +
//                    $"{existingFeiraReceivedFromRepository.Nome} - {existingFeiraReceivedFromRepository.NumeroRegistro}"));
//        }
//        else
//        {
//            getFeiraRepositoryResult.Errors.ToList().ForEach(err =>
//                AddNewFeiraResult.AddError(AddNewFeiraErrors.RepositoryError(err)));
//        }
//    }

//    if (AddNewFeiraResult.HasErrors) return AddNewFeiraResult;

//    var feiraToSave = newFeiraInfos.MapValuesTo<Feira>();
//    var addFeiraRepositoryResult = await _feiraRepository.AddAsync(feiraToSave);

//    if (addFeiraRepositoryResult.IsSuccess())
//        return new AddNewFeiraResult(addFeiraRepositoryResult.Value);

//    addFeiraRepositoryResult.Errors.ToList().ForEach(err =>
//        AddNewFeiraResult.AddError(AddNewFeiraErrors.RepositoryError(err)));

//    return AddNewFeiraResult;
//}

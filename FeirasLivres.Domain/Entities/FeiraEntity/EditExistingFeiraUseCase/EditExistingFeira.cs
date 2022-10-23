﻿using FeirasLivres.Domain.Entities.Common;
using FeirasLivres.Domain.Entities.DistritoEntity;
using FeirasLivres.Domain.Entities.FeiraEntity.AddNewFeiraUseCase;
using FeirasLivres.Domain.Entities.SubPrefeituraEntity;

namespace FeirasLivres.Domain.Entities.FeiraEntity.EditExistingFeiraUseCase
{
    public class EditExistingFeira
    {
        private readonly IFeiraRepository         _feiraRepository;
        private readonly IDistritoRepository      _distritoRepository;
        private readonly ISubPrefeituraRepository _subPrefeituraRepository;

        public EditExistingFeira(IFeiraRepository feiraRepsitory, IDistritoRepository distritoRepository, ISubPrefeituraRepository subPrefeituraRepsitory)
            =>  (_feiraRepository, _distritoRepository, _subPrefeituraRepository) =
                ( feiraRepsitory ,  distritoRepository,  subPrefeituraRepsitory );

        public async Task<IDomainActionResult<bool>> Execute(EditExistingFeiraParams feiraInfosToEdit)
        {
            var paramsValidationResult = new EditExistingFeiraParamsValidator().Validate(feiraInfosToEdit);
            var updateFeiraResult = new DomainActionResult<bool>(paramsValidationResult.Errors);

            if (await DistritoNotFound(feiraInfosToEdit.DistritoId))
                updateFeiraResult.AddError(AddNewFeiraErrors.DistritoNotFound());

            if (await SubPrefeituraNotFound(feiraInfosToEdit.SubPrefeituraId))
                updateFeiraResult.AddError(AddNewFeiraErrors.SubPrefeituraNotFound());

            if (updateFeiraResult.HasErrors) return updateFeiraResult;

            var updateFeiraRepositoryResult = await _feiraRepository.UpdateByNumeroRegistroAsync(feiraInfosToEdit);
            updateFeiraResult.AddErrorsFrom(updateFeiraRepositoryResult);

            return updateFeiraResult.SetValue(updateFeiraRepositoryResult.IsSuccess());
        }

        private async Task<bool> DistritoNotFound(Guid distritoId)
        {
            var resultGetDistritoByIdRepository = await _distritoRepository.GetByIdAsync(distritoId);
            return resultGetDistritoByIdRepository.HasErrors();
        }

        private async Task<bool> SubPrefeituraNotFound(Guid subPrefeituraId)
        {
            var resultGetSubPrefeituraByIdRepository = await _subPrefeituraRepository.GetByIdAsync(subPrefeituraId);
            return resultGetSubPrefeituraByIdRepository.HasErrors();
        }
    }
}

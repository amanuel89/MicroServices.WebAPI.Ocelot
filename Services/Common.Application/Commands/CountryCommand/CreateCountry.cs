//using AutoMapper;
//using Common.Application.Models.Common;
//using CommonService.Application.Services.Helper;
//using CommonService.Domain.Models;

//namespace Common.Application.Commands.CountryCommand;
//public class CreateBanks : IRequest<OperationResult<BankResponseDTO>>
//{
//    public BankRequestDTO Bank { get;  set; }
//}
//public class CreateBanksHandler : IRequestHandler<CreateBanks, OperationResult<BankResponseDTO>>
//{
//    private readonly IRepositoryBase<Bank> _Banks;
//    private readonly IMapper  _mapper;
//    private readonly ImageUploader _imageUploader;
//   // private readonly IRepositoryBase<VerificationCodes> _VerificationCodes;
//    public CreateBanksHandler(IRepositoryBase<Bank> _Banks,IMapper imapper, ImageUploader imageUploader)
//    {
//        this._Banks = _Banks;
//        _mapper = imapper;
//        _imageUploader = imageUploader;
//    }

//    public async Task<OperationResult<BankResponseDTO>> Handle(CreateBanks request, CancellationToken cancellationToken)
//    {
//        var result = new OperationResult<BankResponseDTO>();

//        try
//        {         

//            var req = request.Bank;
//            var bankLogo = "";
//            if (req.BankLogo != null)
//            {
//                using (var memoryStream = new MemoryStream())
//                {
//                    await req.BankLogo.CopyToAsync(memoryStream);
//                    byte[] fileBytes = memoryStream.ToArray();
//                    string fileName = $"{req.Name}_{DateTime.Now:yyyyMMddHHmmss}";
//                    ImageUploadResponse image = _imageUploader.UploadToWwwRoot(fileBytes, fileName, IMAGECATEGORY.OTHER);

//                    if (!image.Success)
//                    {
//                        result.AddError(ErrorCode.ValidationError, "Logo upload has issue: " + image.ErrorMessage);
//                        return result;
//                    }
//                    bankLogo= image.Path;
//                }
//            }     

//            var bank = Bank.Create(req.Name, req.AccountName, req.AccountNumber, bankLogo);
//            await _Banks.AddAsync(bank);

//            var response = _mapper.Map<BankResponseDTO>(bank);
//            result.Payload = response;
//            result.Message = "Operation success";
//        }
//        catch (Exception ex)
//        {
//            result.AddError(ErrorCode.UnknownError, $"error occurred Bank not registered.");
//        }

//        return result;
//    }
//}

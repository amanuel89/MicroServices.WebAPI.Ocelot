using AutoMapper;
using Common.Application.Models.Common;
using ConsigneeService.Domain.Models;

namespace ConsigneeService.Application.Commands;

public class UpdateBank : IRequest<OperationResult<BankResponseDTO>>
{
    public long Id { get; set; }
    public required BankUpdateDTO Bank { get; set; }
}
public class UpdateBankHandler : IRequestHandler<UpdateBank, OperationResult<BankResponseDTO>>
{
    private readonly IRepositoryBase<Bank> _Bank;
    private readonly IMapper _mapper;
    private readonly ImageUploader _imageUploader;
    public UpdateBankHandler(IRepositoryBase<Bank> _Bank, IMapper imapper, ImageUploader imageUploader)
    {
        this._Bank = _Bank;
        this._mapper = imapper;
        _imageUploader = imageUploader;
    }
    public async Task<OperationResult<BankResponseDTO>> Handle(UpdateBank request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<BankResponseDTO>();
        var Bank = _Bank.FirstOrDefault(x => x.Id == request.Id && x.RecordStatus != RecordStatus.Deleted);

        if (Bank == null)
        {
            result.AddError(ErrorCode.NotFound, "Record doesn't exist.");
            return result;
        }

        var req = request.Bank;
        var bankLogo = Bank.BankLogo;
        if (req.BankLogo != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await req.BankLogo.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                string fileName = $"{req.Name}_{DateTime.Now:yyyyMMddHHmmss}";
                ImageUploadResponse image = _imageUploader.UploadToWwwRoot(fileBytes, fileName, IMAGECATEGORY.DRIVERSPHOTO);

                if (!image.Success)
                {
                    result.AddError(ErrorCode.ValidationError, "Logo upload has issue: " + image.ErrorMessage);
                    return result;
                }
                bankLogo = image.Path;
            }
        }

        Bank.Update(req.Name, req.Name,req.AccountNumber, bankLogo);
        _Bank.Update(Bank);
        var response = _mapper.Map<BankResponseDTO>(Bank);
        result.Payload = response;
        result.Message = "Operation success";
        return result;
    }
}

using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SharedLibrary.DTO;
using SharedLibrary.Mappers;
using SharedLibrary.Models;
using WebProgrammingTerm.Core;

using WebProgrammingTerm.Core.Repositories;
using WebProgrammingTerm.Core.Services;
using IUnitOfWork = WebProgrammingTerm.Core.UnitOfWorks.IUnitOfWork;

namespace WebProgrammingTerm.Service.Services;

public class CompanyUserService:GenericService<CompanyUser>,ICompanyUserService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICompanyUserRepository _companyUserRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CompanyUserService(IUnitOfWork unitOfWork, ICompanyUserRepository companyUserRepository, ICompanyRepository companyRepository, IUserRepository userRepository) : base(companyUserRepository,unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _companyUserRepository = companyUserRepository;
        _companyRepository = companyRepository;
        _userRepository = userRepository;
    }


}
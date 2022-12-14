using System.Linq.Expressions;
using AutoMapper;
using Disco.Service.Users.Core.Entities;
using Disco.Service.Users.Core.Repositories;
using Disco.Service.Users.Infrastructure.Mongo.Documents;
using Disco.Shared.Mongo.Repository;

namespace Disco.Service.Users.Infrastructure.Mongo.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly IMongoRepository<UserDocument, Guid> _repository;
    private readonly IMapper _mapper;

    public UserRepository(IMongoRepository<UserDocument,Guid> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<User> GetAsync(Guid id)
    {
        var entity = await _repository.GetAsync(x=>x.Id.Equals(id));
        
        var user = _mapper.Map<User>(entity);
        
        if (user is not null && !string.IsNullOrWhiteSpace(user.PasswordHash))
        {
            user.SetNewPasswordHash(entity.PasswordHash);    
        }
        
        return user;
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _repository.ExistsAsync(x=>x.Id.Equals(id));
    }

    public Task<bool> ExistsByEmailAsync(string email)
    {
        Expression<Func<UserDocument, bool>> expression = x => x.Email.Equals(email);
       return _repository.ExistsAsync(expression);
    }

    public Task AddAsync(User resource)
    {
        var entity = _mapper.Map<UserDocument>(resource);
        return _repository.AddAsync(entity);
    }

    public Task UpdateAsync(User resource)
    {
        var entity = _mapper.Map<UserDocument>(resource);
        return _repository.UpdateAsync(entity);
    }

    public Task DeleteAsync(Guid id)
    {
        return _repository.RemoveAsync(x => x.Id.Equals(id));
    }

    public async Task<User> GetAsyncByEmail(string email)
    {
        var entity = await _repository.GetAsync(x => x.Email == email);
        return _mapper.Map<User>(entity);
    }
}
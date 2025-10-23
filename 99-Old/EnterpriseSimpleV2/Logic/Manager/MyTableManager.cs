using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EnterpriseSimpleV2.Logic.Abstraction;
using EnterpriseSimpleV2.Logic.Abstraction.DTOs;

namespace EnterpriseSimpleV2.Logic.Manager
{
    public class MyTableManager : IMyTableManager
    {
        private IMyTableRepository _repository;
        private IMapper _mapper;

        public MyTableManager(IMyTableRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException();
            _mapper = mapper ?? throw new ArgumentNullException();
        }

        public async Task<IEnumerable<MyTable>> GetAll()
        {
            return _mapper.Map<IEnumerable<MyTable>>(await _repository.GetAll());
        }

        public async Task<MyTable> Get(int id)
        {
            return _mapper.Map<MyTable>(await _repository.Get(id));
        }

        public async Task<int> Add(MyTable value)
        {
            return await _repository.Add(_mapper.Map<Repository.Abstraction.Entities.MyTable>(value));
        }
    }
}
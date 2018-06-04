using System;

namespace Template.Domain.Data
{
    public interface IFirmDataContext : IDataContext
    {
        IClientRepository ClientRepository { get; set; }

        void Init(Guid firmId);
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poketto.Application.Common.Mapping
{
    public interface IMappableFrom<T>
    {
        void CreateMap(Profile profile);
    }
}

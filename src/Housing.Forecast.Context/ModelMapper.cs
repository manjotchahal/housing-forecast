using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Housing.Forecast.Context.Models;
using Housing.Forecast.Library.Models;


namespace Housing.Forecast.Context
{
    public static class ModelMapper
    {

        public static Models.Address MapAddress(Library.Models.Address libAddress)
        {
            var destination = Mapper.Map<Library.Models.Address, Models.Address>(libAddress);
            return destination;
        }

        public static Models.User MapUser(Library.Models.User libUser)
        {
            var destination = Mapper.Map<Library.Models.User, Models.User>(libUser);
            return destination;
        }

        public static Models.Room MapRoom(Library.Models.Room libRoom)
        {
            var destination = Mapper.Map<Library.Models.Room, Models.Room>(libRoom);
            return destination;
        }

        public static Models.Name MapName(Library.Models.Name libName)
        {
            var destination = Mapper.Map<Library.Models.Name, Models.Name>(libName);
            return destination;
        }

        public static Models.Batch MapBatch(Library.Models.Batch libBatch)
        {
            var destination = Mapper.Map<Library.Models.Batch, Models.Batch>(libBatch);
            return destination;
        }

        public static Models.Snapshot MapSnapshot(Library.Models.Snapshot libSnap)
        {
            var destination = Mapper.Map<Library.Models.Snapshot, Models.Snapshot>(libSnap);
            return destination;
        }
    }
}

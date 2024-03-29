﻿using Moq;
using CompanyName.GatewayManagement.Data;
using CompanyName.GatewayManagement.Data.Entities;
using CompanyName.GatewayManagement.Domain.DTO;
using CompanyName.GatewayManagement.Domain.Exceptions;
using CompanyName.GatewayManagement.Domain.Interfaces;
using CompanyName.GatewayManagement.Domain.Services;
using CompanyName.GatewayManagement.Domain.Tests.FakeDBWrapper;
using CompanyName.GatewayManagement.Domain.Tests.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CompanyName.GatewayManagement.Domain.Tests
{
    public class GatewayServiceUnitTest
    {
        private readonly IGatewayService _service;


        public GatewayServiceUnitTest()
        {
            UnitOfWork unitOfWork = InitUnitOfWork();
            _service = new GatewayService(unitOfWork, AutoMapperConfigurationManager.Mapper);
        }

        private UnitOfWork InitUnitOfWork()
        {
            var context = new Mock<GatewayDbContext>();
            var dbWrapper = new FakeConcreteDbWrapper();
            dbWrapper.AddDbset(GetDummyGatewayData(), ref context);
            var unitOfWork = new UnitOfWork(context.Object);
            return unitOfWork;
        }

        [Fact]
        public async Task GetAllGateways_Should_Return_Not_Deleted_Gateways_In_Assending_Order()
        {
            var actual = await _service.GetAllGateways();
            Assert.NotEmpty(actual);
            Assert.Equal(3, actual.Count);

            Assert.Equal("aws.net", actual[0].GatewayName);
            Assert.Equal("dhaka.net", actual[1].GatewayName);
            Assert.Equal("sofia.net", actual[2].GatewayName);
        }

        [Fact]
        public async Task GetAllGateways_Should_Return_Gateway_And_Its_Devices()
        {
            var actual = await _service.GetAllGateways();

            Assert.Equal(10, actual[0].PeripheralDevices.Count);
            Assert.Equal(2, actual[1].PeripheralDevices.Count);
            Assert.Empty(actual[2].PeripheralDevices);
        }


        [Fact]
        public async Task GetGateway_With_Id_Should_Return_A_Gateway()
        {
            var actual = await _service.GetGateway(1);
            Assert.Equal("sofia.net", actual.GatewayName);
        }

        [Fact]
        public async Task GetGateway_With_NON_EXISTING_ID_Should_Throw_Exception()
        {
            var actualException = await Assert.ThrowsAsync<GatewayManagementNotFoundResultException>(() => _service.GetGateway(5));
            Assert.Equal("E0404", actualException.ErrorCode);
        }

        [Fact]
        public async Task AddGateway_Should_Add_Gateway()
        {
            var actual = await _service.AddGateway(new GatewayRequestDto() { AddressIpv4 = "192.168.30.7", GatewayName = "test" });
            Assert.True(actual.Status);
        }




        #region DummyData
        private IEnumerable<Gateway> GetDummyGatewayData()
        {
            var gateways = new List<Gateway>()
            {
                new Gateway {  Id = 1, GatewayName = "sofia.net", AddressIpv4="192.168.0.10", SerialNumber=new Guid("CBD20EC4-8A17-EC11-826C-CD7EFF2D0157"),  IsDeleted=false, CreatedOn = GetDateTimeFromString("2021-09-17T10:19:00"), PeripheralDevices=null },
                new Gateway {  Id = 2, GatewayName = "dhaka.net", AddressIpv4="192.168.20.102", SerialNumber=new Guid("82A805A3-9717-EC11-826C-CD7EFF2D0157"),  IsDeleted=false, CreatedOn = GetDateTimeFromString("2021-09-17T12:09:48"), PeripheralDevices=GetTwoDummyDevices()},
                new Gateway {  Id = 4, GatewayName = "aws.net", AddressIpv4="192.168.0.102", SerialNumber=new Guid("7D1B8B36-7F19-EC11-826F-C2DE2BAA7110"),  IsDeleted=false, CreatedOn = GetDateTimeFromString("2021-09-18T10:19:48"), PeripheralDevices=GetTenDummyDevices() },
                new Gateway {  Id = 3, GatewayName = "roma.net", AddressIpv4="192.168.0.10", SerialNumber=new Guid("EBD20EC4-8A17-EC22-826C-CD7EFF2D0167"),  IsDeleted=true, CreatedOn = GetDateTimeFromString("2021-09-17T11:09:48"), PeripheralDevices=null }
            };

            return gateways.Select(_ => _);
        }

        private List<PeripheralDevice> GetTwoDummyDevices()
        {
            return new List<PeripheralDevice>()
            { new PeripheralDevice(){  VendorName="Lenovo", IsDeleted=false  } ,
              new PeripheralDevice(){  VendorName="Dell", IsDeleted=false  }
            };
        }
        private List<PeripheralDevice> GetTenDummyDevices()
        {
            return new List<PeripheralDevice>()
            {
              new PeripheralDevice(){  VendorName="Lenovo", IsDeleted=false  } ,
              new PeripheralDevice(){  VendorName="Dell", IsDeleted=false  },
              new PeripheralDevice(){  VendorName="Varizon", IsDeleted=false  } ,
              new PeripheralDevice(){  VendorName="Aer", IsDeleted=false  },
              new PeripheralDevice(){  VendorName="IPhone", IsDeleted=false  } ,
              new PeripheralDevice(){  VendorName="Sun", IsDeleted=false  },
              new PeripheralDevice(){  VendorName="Oracle", IsDeleted=false  } ,
              new PeripheralDevice(){  VendorName="Microsoft", IsDeleted=false  },
              new PeripheralDevice(){  VendorName="Sony", IsDeleted=false  } ,
              new PeripheralDevice(){  VendorName="RealMe", IsDeleted=false  }
            };
        }
        #endregion
        private DateTime GetDateTimeFromString(string givenDateTime)
        {
            return DateTime.ParseExact(givenDateTime, "yyyy-MM-ddTHH:mm:ss", null);
        }

    }
}

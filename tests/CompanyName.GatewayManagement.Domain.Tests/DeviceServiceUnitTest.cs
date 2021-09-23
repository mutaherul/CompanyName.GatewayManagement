using Moq;
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
using System.Threading.Tasks;
using Xunit;

namespace CompanyName.GatewayManagement.Domain.Tests
{
    public class DeviceServiceUnitTest
    {
        private readonly IDeviceService _service;


        public DeviceServiceUnitTest()
        {
            UnitOfWork unitOfWork = InitUnitOfWork();
            _service = new DeviceService(unitOfWork, AutoMapperConfigurationManager.Mapper);
        }

        private UnitOfWork InitUnitOfWork()
        {
            var context = new Mock<GatewayDbContext>();
            var dbWrapper = new FakeConcreteDbWrapper();
            dbWrapper.AddDbset(GetDummyDeviceData(), ref context);
            dbWrapper.AddDbset(GetDummyGatewayData(), ref context);
            var unitOfWork = new UnitOfWork(context.Object);
            return unitOfWork;
        }


        [Fact]
        public async Task AddDevice_Should_Add_Device()
        {
            var actual = await _service.AddDevice(1, new DeviceRequestDto() { Status = 1, VendorName = "lenovo" });
            Assert.True(actual.Status);
        }

        [Fact]
        public async Task AddDevice_With_Invalid_Gateway_Id_Should_Not_Allow_Add_Any_New_Device()
        {
            var actualException = await Assert.ThrowsAsync<GatewayManagementNotFoundResultException>(() => _service.AddDevice(10, new DeviceRequestDto() { Status = 1, VendorName = "lenovo" }));
            Assert.Equal("E0404", actualException.ErrorCode);
        }

        [Fact]
        public async Task AddDevice_On_A_Gateway_Which_Has_Already_Ten_Device_Should_Not_Allow_Add_Any_New_Device()
        {
            var actualException = await Assert.ThrowsAsync<GatewayManagementForbiddenRequestException>(() => _service.AddDevice(4, new DeviceRequestDto() { Status = 1, VendorName = "lenovo" }));
            Assert.Equal("S001", actualException.ErrorCode);
        }

        [Fact]
        public async Task GetDevice_With_Id_Should_Return_A_Device()
        {
            var actual = await _service.GetDevice(101);
            Assert.Equal("Dell", actual.VendorName);
            Assert.Equal(101, actual.Uid);
        }

        [Fact]
        public async Task GetDevice_With_NON_EXISTING_ID_Should_Throw_Exception()
        {
            var actualException = await Assert.ThrowsAsync<GatewayManagementNotFoundResultException>(() => _service.GetDevice(500));
            Assert.Equal("E0404", actualException.ErrorCode);
        }


        #region DummyData
        private IEnumerable<PeripheralDevice> GetDummyDeviceData()
        {
            var devices = new List<PeripheralDevice>()
            {
              new PeripheralDevice(){  Uid=100, VendorName="Lenovo", IsDeleted=false  } ,
              new PeripheralDevice(){  Uid=101, VendorName="Dell", IsDeleted=false  },
              new PeripheralDevice(){  Uid=102, VendorName="Varizon", IsDeleted=false  } ,
              new PeripheralDevice(){  Uid=103, VendorName="Aer", IsDeleted=false  },
              new PeripheralDevice(){  Uid=104, VendorName="IPhone", IsDeleted=false  } ,
              new PeripheralDevice(){  Uid=105, VendorName="Sun", IsDeleted=false  },
              new PeripheralDevice(){  Uid=106, VendorName="Oracle", IsDeleted=false  } ,
              new PeripheralDevice(){  Uid=107, VendorName="Microsoft", IsDeleted=false  },
              new PeripheralDevice(){  Uid=108, VendorName="Sony", IsDeleted=false  } ,
              new PeripheralDevice(){  Uid=109, VendorName="RealMe", IsDeleted=false  },
              new PeripheralDevice(){  Uid=110, VendorName="Huwai", IsDeleted=true  }
            };

            return devices;
        }



        private IEnumerable<Gateway> GetDummyGatewayData()
        {
            var gateways = new List<Gateway>()
            {
                new Gateway {  Id = 1, GatewayName = "sofia.net", AddressIpv4="192.168.0.10", SerialNumber=new Guid("CBD20EC4-8A17-EC11-826C-CD7EFF2D0157"),  IsDeleted=false, CreatedOn = GetDateTimeFromString("2021-09-17T10:19:00"), PeripheralDevices=null },
                new Gateway {  Id = 4, GatewayName = "aws.net", AddressIpv4="192.168.0.102", SerialNumber=new Guid("7D1B8B36-7F19-EC11-826F-C2DE2BAA7110"),  IsDeleted=false, CreatedOn = GetDateTimeFromString("2021-09-18T10:19:48"), PeripheralDevices=GetTenDummyDevices() },
                new Gateway {  Id = 3, GatewayName = "roma.net", AddressIpv4="192.168.0.10", SerialNumber=new Guid("EBD20EC4-8A17-EC22-826C-CD7EFF2D0167"),  IsDeleted=true, CreatedOn = GetDateTimeFromString("2021-09-17T11:09:48"), PeripheralDevices=null }
            };

            return gateways;
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

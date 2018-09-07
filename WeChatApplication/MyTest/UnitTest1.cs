using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Web;
using WebApiEnum;

namespace MyTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string url = "https://crmtestapi.e6yun.com/Web/AddWeiXinMessage";
            string postData = "jsonData=\"\"";
            string result = HttpHelper.Http(url, postData, MyHttpMethod.POST);
        }

        [TestMethod]
        public void TestDocket()
        {
            string url = "http://172.16.57.22:8001/api/Web/SyndocketInfo";
            string employeNo = "201410080932";
            var anonymous = new
            {
                active_id = 43246m,
                corp_id = 10034m,
                corp_name = "华子运输公司",
                doc_state = 1,
                doc_apply_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                doc_approval_time = "",
                current_node = "审批中",
                applicant_no = employeNo,
                applicant_name = "蒋丽",
                doc_type = 14m,
                msg = Utility.EncryptDES(HttpUtility.UrlEncode(employeNo)),
                corp_diff = 2m
            };
            string json = JsonConvert.SerializeObject(anonymous);
            string data = "jsonData=" + HttpUtility.UrlEncode(Utility.EncryptDES(json));
            string result = HttpHelper.Http(url, data, MyHttpMethod.POST);
        }

        [TestMethod]
        public void TestDocContract()
        {
            string url = "http://172.16.57.22:8001/api/Web/SynContractInfo";
            decimal activeId = 43244m;
            var anonymous = new
            {
                contract_no = "E6GDGD20180810E3001",
                contract_type = 3,
                is_standard = 0,
                is_delay = 0,
                examine_status = 0,
                template_name = "这是个套餐名字",
                template_id = 10,
                delay_time = "",
                active_id = activeId,
                msg = Utility.EncryptDES(HttpUtility.UrlEncode(activeId.ToString())),
            };
            string json = JsonConvert.SerializeObject(anonymous);
            string data = "jsonData=" + HttpUtility.UrlEncode(Utility.EncryptDES(json));
            string result = HttpHelper.Http(url, data, MyHttpMethod.POST);
        }

        [TestMethod]
        public void TestSynContractPostState()
        {
            string url = "http://172.16.57.22:8001/api/Web/SynContractPostState";
            string contractNo = "E6GDGD20180810E3001";
            var anonymous = new
            {
                post_state = 1,
                contract_no = contractNo,
                msg = Utility.EncryptDES(HttpUtility.UrlEncode(contractNo))
            };
            string json = JsonConvert.SerializeObject(anonymous);
            string data = "jsonData=" + HttpUtility.UrlEncode(Utility.EncryptDES(json));
            string result = HttpHelper.Http(url, data, MyHttpMethod.POST);
        }

        [TestMethod]
        public void TestAddTaskInfo()
        {
            string url = "https://crmtestapi.e6yun.com/Web/AddTaskInfo";
            string json = "mdU6XhGDUeeOf/3ujCktDFLHUyAPGUOL8ag1uHplDdN681+4KeriVPCbnUDJu/t2Jn5jUh3NjVn3nw5WowE/vCIFNoEE4OSc+ZDj8UPa4D1MFPU1qXANOxh7G2GAfWa4Es/A8C7jPvNm6+0Xnlw6ybpchxMWtrHfgrcio8PPAxKiaXi5McgrRTxJ3zjvu6l8r8lHd/rHZiSsj7RQ3Qrg1AMMeXz5v7U9Hlh0/J3xbUJWiBKZYQCCiyP1UcbliDmTMxKVUuQClNMqmcOxNCxsgaCPYCM7+4UFuf0XjTkfnXU/1SoQMVkkDXPGk3WZZXF1OWD3PjSE/zoXcMWrpGbzE/yqFtjeiqxA6OE8X5thqsWiI5CFfsuxSp+4BB3krIvt3vpDSdnnLsmxEGpMo3fpEC4gUpUuS7riG+/Ci0OR+Ym4T6ahiR3u+IhftSh/r4MxC/k6W1tFTZ1C6Gfv/VIXT8L0XwwRfUMpYxZCck+Nx5hJyG9jojsLNbK8gA3/d0RH9W2tLYdsWbZVDp2EpbnyR0QbYawj8j0sKIeOS6jp3DAiRJN/4aFONiObBipPrczH65LIJoRC+m3FXaOn6ZkP4Q==";
            string data = "jsonData=" + HttpUtility.UrlEncode(json);
            string result = HttpHelper.Http(url, data, MyHttpMethod.POST);
        }
    }
}

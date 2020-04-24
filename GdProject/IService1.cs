using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GdProject.Models;

namespace GdProject
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        User GetUser(string userid);

        [OperationContract]
        void PostUser(User user);

        [OperationContract]
        int PostGroup(Group grp);

        [OperationContract]
        void AddToGroup(int grpid, string userid);

        [OperationContract]
        List<GroupUser> GetAllUserGroups(string userid);

        // TODO: Add your service operations here
        [OperationContract]
        List<String> GetSameGroupUser(int GroupId);

        [OperationContract]
        void DeleteFromGroup(int grpid, string userid);

        [OperationContract]
        List<int> GetAllAdminGroup(string userid);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "GdProject.ContractType".
}

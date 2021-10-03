/* eslint-disable */
module.exports = {
    COMMODITY: {
        Api: 'https://localhost:44350/api/v1/Commoditys/Filter?pageIndex=1&pageSize=20',
        Style: [
            {name: 'Mã hàng', field: 'commodity_code'},
            {name: 'Tên hàng', field: 'commodity_name'},
            {name: 'Số lượng tồn', field: 'debit_amount'}
        ]
    },
    WAREHOUSE: {
        Api: 'https://localhost:44350/api/v1/Warehouses/Filter?pageIndex=1&pageSize=20',
        Style: [
            {name: 'Mã kho', field: 'warehouse_code'},
            {name: 'Tên kho', field: 'warehouse_name'}
        ]
    },
    EMPLOYEE: {
        Api:  'https://localhost:44350/api/v1/Employees/Filter?pageIndex=1&pageSize=20',
        Style: [
            {name: 'Mã nhân viên', field: 'employee_code'},
            {name: 'Tên nhân viên', field: 'employee_name'},
            {name: 'Đơn vị', field: 'department_name'},
            {name: 'ĐT di động', field: 'phone_number'},
        ]
    },
    OBJECT: {
        Api: 'https://localhost:44350/api/v1/AccountObjects/Filter?pageIndex=1&pageSize=20',
        Style: [
            {name: 'Mã đối tượng', field: 'account_object_code'}, 
            {name: 'Tên đối tượng', field: 'account_object_name'},
            {name: 'Mã số thuế', field: 'tax_code'},
            {name: 'Địa chỉ', field: 'contact_address'},
            {name: 'Số điện thoại', field: 'phone_number'}
        ]
    },
    DEBIT_ACCOUNT: {
        Api: 'https://localhost:44350/api/v1/Accounts/Filter?pageIndex=1&pageSize=20&type=1',
        Style: [
            {name: 'Số tài khoản', field: 'account_number'},
            {name: 'Tên tài khoản', field: 'account_name'},
        ]
    },
    CREDIT_ACCOUNT: {
        Api: 'https://localhost:44350/api/v1/Accounts/Filter?pageIndex=1&pageSize=20&type=6',
        Style: [
            {name: 'Số tài khoản', field: 'account_number'},
            {name: 'Tên tài khoản', field: 'account_name'},
        ]
    },
    ACCOUNT_OBJECT_GROUP: {
        Api: 'https://localhost:44350/api/v1/AccountObjectGroups/Filter?pageIndex=1&pageSize=20',
        Style: [
            {name: 'Tên nhóm khách hàng', field: 'account_object_group_name'},
            {name: 'Mã nhóm khách hàng', field: 'account_object_group_code'},
        ]

    },
    COMMODITY_GROUP: {
        Api: 'https://localhost:44350/api/v1/CommodityGroups/Filter?pageIndex=1&pageSize=20',
        Style: [
            {name: 'Tên nhóm vật tư, hàng hoá, dịch vụ', field: 'commodity_group_name'},
            {name: 'Mã nhóm vật tư, hàng hoá, dịch vụ', field: 'commodity_group_code'},
        ]
    },
    DEPARTMENT: {
        Api: 'https://localhost:44350/api/v1/Departments/Filter?pageIndex=1&pageSize=20', 
        Style: [
            {name: 'Tên phòng ban', field: 'department_name'},
            {name: 'Mã phòng ban', field: 'department_code'},
        ]
    },
    UNIT: {
        Api: 'https://localhost:44350/api/v1/Units/Filter?pageIndex=1&pageSize=20', 
        Style: [
            {name: 'Tên đơn vị tính', field: 'unit_name'},
            {name: 'Mã đơn vị tính', field: 'unit_code'},
        ]
    },
    VOCATIVE: {
        Api: 'https://localhost:44350/api/v1/Vocatives/Filter?pageIndex=1&pageSize=20', 
        Style: [
            {name: 'Tên Xưng hô', field: 'vocative_name'},
        ]
    }
}
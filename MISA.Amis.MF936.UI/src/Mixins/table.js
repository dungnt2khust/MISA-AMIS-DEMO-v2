export default {
    data() {
        return {
            // TYPE: 0 - Text, 1 - Checkbox
            // POSITION: 0 - Left, 1 - Center, 2 - Right
            tableEmployeeStyle: [
                {name: 'Mã nhân viên', field: 'EmployeeCode', type: 0, pos: 0},
                {name: 'Tên nhân viên', field: 'FullName', type: 0, pos: 0},
                {name: 'Giới tính', field: 'Gender',type: 0, pos: 0},
                {name: 'Ngày sinh', field: 'DateOfBirth',type: 0, pos: 1},
                {name: 'Số CMND/ CCCD', field: 'IdentityNumber',type: 0, pos: 0},
                {name: 'Email', field: 'Email',type: 0, pos: 0},
                {name: 'Số điện thoại', field: 'PhoneNumber',type: 0, pos: 0},
                {name: 'Chức danh', field: 'PositionName',type: 0, pos: 0},
                {name: 'Đơn vị', field: 'DepartmentName',type: 0, pos: 0},
                {name: 'Là khách hàng', field: 'IsCustomer', type: 1, pos: 0}, 
                {name: 'Là nhà cung cấp', field: 'IsSupplier', type: 1, pos: 0},
                {name: 'Số tài khoản', field: 'BankAccount',type: 0, pos: 0},
                {name: 'Tên ngân hàng', field: 'BankName',type: 0, pos: 0},
                {name: 'Chi nhánh ngân hàng', field: 'BankBranch',type: 0, pos: 0}
            ],
            tableCustomerStyle: [
                {name: 'Mã khách hàng', field: 'CustomerCode', type: 0, pos: 0},
                {name: 'Tên khách hàng', field: 'FullName', type: 0, pos: 0},
                {name: 'Giới tính', field: 'Gender',type: 0, pos: 0},
                {name: 'Ngày sinh', field: 'DateOfBirth',type: 0, pos: 1},
                {name: 'Số CMND/ CCCD', field: 'IdentityNumber',type: 0, pos: 0},
                {name: 'Email', field: 'Email',type: 0, pos: 0},
                {name: 'Số điện thoại', field: 'PhoneNumber',type: 0, pos: 0}, 
                {name: 'Nhóm khách hàng', field: 'CustomerGroupName',type: 0, pos: 0},
                {name: 'Số tài khoản', field: 'BankAccount',type: 0, pos: 0},
                {name: 'Tên ngân hàng', field: 'BankName',type: 0, pos: 0},
                {name: 'Chi nhánh ngân hàng', field: 'BankBranch',type: 0, pos: 0}
            ]
        }
    }, 
    methods: {
        /**
         * Tính giá trị của một ô dữ liệu
         * @param {Object} data
         * @param {Object} style
         * @returns {Number, String} 
         * CreatedBy: NTDUNG (01/09/2021)
         */
        tdValue(data, style) {
            // Nếu là checkbox thì không in gì cả
            if (style['type'] == 1) {
                return "";
            } else {
                // Nếu trường là giới tính thì hiển thị
                if (style['field'].includes('Gender'))
                    return this.getGenderName(data[style['field']]);
                
                // Nếu trường là date thì format lại
                if (style['field'].includes('Date')) {
                    return this.formatDate(data[style['field']]);
                }

                // Còn lại in ra bình thường
                return data[style['field']];
            }	
        },
        /**
         * Trả về tên giới tính
         * @param {int} type mã giới tính
         * @returns {string} tên giới tính
         * CreatedBy: NTDUNG (01/09/2021)
         */
        getGenderName(type){
            switch(type) {
                case 0: 
                    return 'Nam';
                case 1:
                    return 'Nữ';
                case 2: 
                    return 'Khác';
            }
        }, 
        /**
         * Format dữ liệu ngày tháng
         * @param {string} value
         */
        formatDate(value) {
            if (value) {
                var currDate = new Date(value);
                var date = currDate.getDate();
                var month = currDate.getMonth() + 1;
                var year = currDate.getFullYear();

                return `${date}/${month}/${year}`;
            }
            return null;
        },
        /**
         * Lấy ra vị trí text của td theo mã => class
         * @param {Number} pos
         * CreatedBy: NTDUNG (01/09/2021)
         */
        getTdPos(pos) {
            switch(pos) {
                case 0:
                    return 'text-align-left';
                case 1: 
                    return 'text-align-center';
                case 2:
                    return 'text-align-right';
                default:
                    return '';
            }
        }
    }
}
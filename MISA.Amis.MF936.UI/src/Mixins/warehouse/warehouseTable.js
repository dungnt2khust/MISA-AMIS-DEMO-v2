import TableDataStyle from "../../js/enum/tableDataStyle";

export default {
	data() {
		return {
			WAREHOUSE_TABLE: {
				InwardOutwardListTitle: "Nhập, xuất kho",
				InwardOutwardList: [
					{
						name: "Ngày hạch toán",
						field: "mathematics_date",
						type: TableDataStyle.TYPE.Text,
						pos: TableDataStyle.POS.Left,
					},
					{
						name: "Số chứng từ",
						field: "voucher_code",
						type: TableDataStyle.TYPE.Text,
						pos: TableDataStyle.POS.Left,
						color: TableDataStyle.COLOR.Blue,
					},
					{
						name: "Diễn giải",
						field: "description",
						width: "400px",
						type: TableDataStyle.TYPE.Text,
						pos: TableDataStyle.POS.Left,
					},
					{
						name: "Tổng tiền",
						field: "total_price",
						type: TableDataStyle.TYPE.Text,
						pos: TableDataStyle.POS.Right,
					},
					{
						name: "Người giao/Người nhận",
						field: "contact_name",
						type: TableDataStyle.TYPE.Text,
						pos: TableDataStyle.POS.Left,
					},
					{
						name: "Ngày chứng từ",
						field: "voucher_date",
						type: TableDataStyle.TYPE.Text,
						pos: TableDataStyle.POS.Left,
						color: TableDataStyle.COLOR.AllGreen,
					},
					{
						name: "Loại chứng từ",
						field: "voucher_type",
						type: TableDataStyle.TYPE.Text,
						pos: TableDataStyle.POS.Left,
					},
					{
						name: "Ngày tạo",
						field: "created_date",
						type: TableDataStyle.TYPE.Text,
						pos: TableDataStyle.POS.Left,
					},
					{
						name: "Người tạo",
						field: "created_by",
						type: TableDataStyle.TYPE.Text,
						pos: TableDataStyle.POS.Left,
					},
					{
						name: "Ngày chỉnh sửa",
						field: "modified_date",
						type: TableDataStyle.TYPE.Text,
						pos: TableDataStyle.POS.Left,
					},
					{
						name: "Người chỉnh sửa",
						field: "modified_by",
						type: TableDataStyle.TYPE.Text,
						pos: TableDataStyle.POS.Left,
					},
				],
				InWardDetail: {
					TABLE_INPUT: [
						{
							name: "Mã hàng",
							field: "commodity_code",
							width: "150px",
							type: TableDataStyle.TYPE.Combobox,
							pos: TableDataStyle.POS.Left,
							style: [
								{ name: "Mã hàng", field: "commodity_code" },
								{ name: "Tên hàng", field: "commodity_name" },
								{ name: "Số lượng tồn", field: "quantity" , pos: this.$enum.POS.Right},
							],
							vmodel: "commodity_id",
							controller: "Commoditys",
							hasFooter: true,
							syncfield: "Commodity",
							form: "showWarehouseAddCommodity",
							required: true,
						},
						{
							name: "Tên hàng",
							field: "commodity_name",
							width: "300px",
							type: TableDataStyle.TYPE.Input,
							pos: TableDataStyle.POS.Left,
							enable: true,
							required: true,
						},
						{
							name: "Kho",
							field: "warehouse_code",
							width: "120px",
							type: TableDataStyle.TYPE.Combobox,
							pos: TableDataStyle.POS.Left,
							style: [
								{ name: "Mã kho", field: "warehouse_code" },

								{ name: "Tên kho", field: "warehouse_name" },
							],
							vmodel: "warehouse_id",
							hasFooter: true,
							controller: "Warehouses",
							form: "showWarehouseAdd",
							required: true,
						},
						{
							name: "TK nợ",
							field: "debit_account_number",
							width: "120px",
							type: TableDataStyle.TYPE.ComboboxNotAdd,
							style: [
								{ name: "Số tài khoản", field: "account_number" },
								{ name: "Tên tài khoản", field: "account_name" },
							],
							vmodel: "debit_account_id",
							subfield: "account_id",
							display: "account_number",
							controller: "Accounts",
							pos: TableDataStyle.POS.Left,
							required: true,
						},
						{
							name: "TK có",
							field: "credit_account_number",
							width: "120px",
							type: TableDataStyle.TYPE.ComboboxNotAdd,
							pos: TableDataStyle.POS.Left,
							style: [
								{ name: "Số tài khoản", field: "account_number" },
								{ name: "Tên tài khoản", field: "account_name" },
							],
							vmodel: "credit_account_id",
							subfield: "account_id",
							display: "account_number",
							controller: "Accounts",
							required: true,
						},
						{
							name: "ĐVT",
							field: "selected_unit_id",
							width: "150px",
							type: TableDataStyle.TYPE.Combobox,
							pos: TableDataStyle.POS.Left,
							data: "units",
							vmodel: "selected_unit_id",
							default: "selected_unit_name",
							subfield: "unit_id",
							display: "unit_name",
							controller: "Units",
							syncfield: "Unit",
							style: [
								{ name: "Tên đơn vị", field: "unit_name" },
								{ name: "Tỷ lệ chuyển đổi", field: "rate" },
							],
							required: true,
						},
						{
							name: "Số lượng",
							field: "quantity",
							width: "150px",
							type: TableDataStyle.TYPE.Input,
							pos: TableDataStyle.POS.Right,
							total: true,
							enable: true,
							required: true,
							format: "money",
							default: 1
						},
						{
							name: "Đơn giá",
							field: "debit_amount",
							width: "150px",
							type: TableDataStyle.TYPE.Input,
							pos: TableDataStyle.POS.Right,
							enable: true,
							required: true,
							format: "money"
						},
						{
							name: "Thành tiền",
							field: "total_price",
							width: "150px",
							type: TableDataStyle.TYPE.Input,
							pos: TableDataStyle.POS.Right,
							total: true,
							enable: true,
							format: "money"
						},
						{
							name: "Hạn sử dụng",
							field: "expiry",
							width: "150px",
							type: TableDataStyle.TYPE.InputDate,
							pos: TableDataStyle.POS.Center,
							overtoday: true,
						},
					],
					OBJECT: {
						name: "Đối tượng",
						field: "account_object_name",
						width: "150px",
						pos: TableDataStyle.POS.Left,
						style: [
							{ name: "Mã đối tượng", field: "account_object_code" },
							{ name: "Tên đối tượng", field: "account_object_name" },
							{ name: "Mã số thuế", field: "tax_code" },
							{ name: "Địa chỉ", field: "contact_address" },
							{ name: "Số điện thoại", field: "phone_number" },
						],
						vmodel: "accountobject_id",
						controller: "AccountObjects",
						form: "showAccountObjectDetail",
						// syncfield: "AccountObject"
					},
					EMPLOYEE: {
						name: "Nhân viên",
						field: "employee_name",
						width: "150px",
						pos: TableDataStyle.POS.Left,
						style: [
							{ name: "Mã nhân viên", field: "employee_code" },
							{ name: "Tên nhân viên", field: "employee_name" },
							{ name: "Đơn vị", field: "department_name" },
							{ name: "ĐT di động", field: "phone_number" },
						],
						vmodel: "employee_id",
						controller: "Employees",
						form: "showEmployeeDetail",
					},
					ACCOUNT_OBJECT_GROUP: {
						field: "account_object_group_name",
						pos: TableDataStyle.POS.Left,
						style: [
							{
								name: "Tên nhóm khách hàng",
								field: "account_object_group_name",
							},
							{
								name: "Mã nhóm khách hàng",
								field: "account_object_group_code",
							},
						],
						vmodel: "accountobjectgroup_id",
						controller: "AccountObjectGroups",
						form: "showAccountObjectGroupDetail",
					},
					WAREHOUSE_ACCOUNT: {
						field: "warehouse_account_number",
						width: "150px",
						type: TableDataStyle.TYPE.ComboboxNotAdd,
						style: [
							{ name: "Số tài khoản", field: "account_number" },
							{ name: "Tên tài khoản", field: "account_name" },
						],
						vmodel: "warehouse_account_id",
						subfield: "account_id",
						display: "account_number",
						controller: "Accounts",
						pos: TableDataStyle.POS.Left,
					},
					WAREHOUSE: {
						name: "Kho",
						field: "warehouse_code",
						width: "150px",
						type: TableDataStyle.TYPE.Combobox,
						pos: TableDataStyle.POS.Left,
						style: [
							{ name: "Mã kho", field: "warehouse_code" },
							{ name: "Tên kho", field: "warehouse_name" },
						],
						vmodel: "warehouse_id",
						hasFooter: true,
						controller: "Warehouses",
						form: "showWarehouseAdd",
					},
					DEBIT_ACCOUNT: {
						field: "debit_account_number",
						width: "150px",
						type: TableDataStyle.TYPE.ComboboxNotAdd,
						style: [
							{ name: "Số tài khoản", field: "account_number" },
							{ name: "Tên tài khoản", field: "account_name" },
						],
						vmodel: "debit_account_id",
						subfield: "account_id",
						display: "account_number",
						controller: "Accounts",
						pos: TableDataStyle.POS.Left,
					},
					CREDIT_ACCOUNT: {
						field: "credit_account_number",
						width: "150px",
						type: TableDataStyle.TYPE.ComboboxNotAdd,
						style: [
							{ name: "Số tài khoản", field: "account_number" },
							{ name: "Tên tài khoản", field: "account_name" },
						],
						vmodel: "credit_account_id",
						subfield: "account_id",
						display: "account_number",
						controller: "Accounts",
						pos: TableDataStyle.POS.Left,
					},
					DEPARTMENT: {
						field: "department_name",
						width: "150px",
						type: TableDataStyle.TYPE.Combobox,
						pos: TableDataStyle.POS.Left,
						style: [
							{ name: "Tên phòng ban", field: "department_name" },
							{ name: "Mã phòng ban", field: "department_code" },
						],
						vmodel: "department_id",
						controller: "Departments",
					},
					UNIT: {
						field: "unit_name",
						width: "150px",
						type: TableDataStyle.TYPE.Combobox,
						pos: TableDataStyle.POS.Left,

						vmodel: "unit_id",
						syncfield: "ConversionUnitMaster",
						controller: "Units",
						form: "showWarehouseAddUnit",
						style: [
							{ name: "Tên đơn vị tính", field: "unit_name" },
							{ name: "Mã đơn vị tính", field: "unit_code" },
						],
						escapeField: 'unit_id',
						required: true,
					},
					CONVERSION_UNIT: [
						{
							name: "Đơn vị chuyển đổi",
							field: "unit_name",
							width: "150px",
							type: TableDataStyle.TYPE.Combobox,
							pos: TableDataStyle.POS.Left,
							style: [
								{ name: "Tên đơn vị tính", field: "unit_name" },
								{ name: "Mã đơn vị tính", field: "unit_code" },
							],
							vmodel: "unit_id",
							controller: "Units",
							hasFooter: true,
							syncfield: "ConversionUnitDetail",
							escapeField: 'unit_id',
							form: "showUnitAddCommodity",
							required: true,
						},
						{
							name: "Tỷ lệ Chuyển đổi",
							field: "rate",
							width: "150px",
							type: TableDataStyle.TYPE.Input,
							pos: TableDataStyle.POS.Right,
							syncfield: "ConversionUnitDetail",
							enable: true,
							required: true,
						},
						{
							name: "Mô tả",
							field: "description",
							width: "300px",
							type: TableDataStyle.TYPE.Input,
							pos: TableDataStyle.POS.Left,
							enable: false,
						},
					],
					COMMODITY_GROUP: {
						field: "commoditygroup_id",
						width: "150px",
						type: TableDataStyle.TYPE.Combobox,
						pos: TableDataStyle.POS.Left,
						style: [
							{
								name: "Tên nhóm vật tư, hàng hoá, dịch vụ",
								field: "commodity_group_name",
							},
							{
								name: "Mã nhóm vật tư, hàng hoá, dịch vụ",
								field: "commodity_group_code",
							},
						],
						vmodel: "commoditygroup_id",
						controller: "CommodityGroups",
						form: "showWarehouseAddCommodityGroup",
					},
					VOCATIVE: {
						field: "vocative_name",
						width: "150px",
						type: TableDataStyle.TYPE.Combobox,
						pos: TableDataStyle.POS.Left,
						style: [{ name: "Tên Xưng hô", field: "vocative_name" }],
						vmodel: "vocative_id",
						controller: "Vocatives",
					},
				},
			},
		};
	},
};

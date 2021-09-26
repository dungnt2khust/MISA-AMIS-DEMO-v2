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
						color: TableDataStyle.COLOR.Blue
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
						name: "Đã lập CT bán hàng",
						field: "is_mention",
						type: TableDataStyle.TYPE.Text,
						pos: TableDataStyle.POS.Left,
					},
					{
						name: "Ngày chứng từ",
						field: "voucher_date",
						type: TableDataStyle.TYPE.Text,
						pos: TableDataStyle.POS.Left,
						color: TableDataStyle.COLOR.AllGreen
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
			},
		};
	},
};

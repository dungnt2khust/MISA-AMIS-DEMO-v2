export default {
    data() {
        return {
            WAREHOUSE_TABLE: {
                InwardOutwardListTitle: 'Nhập, xuất kho',
                InwardOutwardList: [
                    {name: 'Ngày hạch toán', field: 'mathematics_date', type: 0, pos: 0},
                    {name: 'Số chứng từ', field: 'voucher_code', type: 2, pos: 0},
                    {name: 'Diễn giải', field: 'description',type: 0, pos: 0},
                    {name: 'Tổng tiền', field: 'summary_money',type: 0, pos: 0},
                    {name: 'Người giao/Người nhận', field: 'account_object_name',type: 0, pos: 0},
                    {name: 'Đã lập CT bán hàng', field: 'is_mention',type: 0, pos: 0},
                    {name: 'Loại chứng từ', field: 'voucher_type',type: 0, pos: 0},
                    {name: 'Loại chứng từ', field: 'voucher_type',type: 0, pos: 0},
                    {name: 'Loại chứng từ', field: 'voucher_type',type: 0, pos: 0},
                ]
            }
        }
    }
}
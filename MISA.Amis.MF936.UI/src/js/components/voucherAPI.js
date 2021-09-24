import BaseAPI from "../base/baseAPI.js";
import axios from "axios";
class VoucherAPI extends BaseAPI {
	constructor() {
		super();
		this.controller = "AccountVouchers";
	}
	/**
	 * lọc và phân trang chứng từ
	 * url phân trang
	 * @returns promise
	 * CreatedBy: NTDUNG(24/9/2021)
	 */
	getAccountVoucherPagingFilter(
		searchData,
		mentionState,
		voucherType,
		startDate,
		endDate,
		pageIndex,
		pageSize
	) {
		let url =
			this.baseApiUrl +
			this.controller +
			`/filter?searchData=${searchData}&mentionState=${mentionState}`;
		url += `&voucherType=${voucherType}&startDate=${startDate}&endDate=${endDate}&pageIndex=${pageIndex}&pageSize=${pageSize}`;
		return axios.get(url);
	}
}
export default new VoucherAPI();

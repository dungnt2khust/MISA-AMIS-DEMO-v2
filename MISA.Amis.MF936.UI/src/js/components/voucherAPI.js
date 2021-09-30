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
	/**
	 * Ghi sổ nhiều
	 * @param {Array} body 
	 * @returns {Promise}
	 * CreatedBy: NTDUNG (28/09/2021)
	 */
	async mentionMany(body) {
		let url = this.baseApiUrl + this.controller + '/mention';
		return await axios.put(url, body);
	}
	/**
	 * Bỏ ghi nhiều
	 * @param {Array} body 
	 * @returns {Promise}
	 * CreatedBy: NTDUNG (28/09/2021)
	 */
	async unMentionMany(body) {	
		let url = this.baseApiUrl + this.controller + '/unmention';
		return await axios.put(url, body);
	}
	/**
     * Lấy chi tiết chứng từ
     * @param {String} voucherId 
     * @returns {Promise}
	 * CreatedBy: NTDUNG (28/09/2021)
     */
    getVoucherDetail(voucherId){
        let url = this.baseApiUrl + this.controller + `/Detail/${voucherId}`;
        return axios.get(url);
    }
	/**
	 * Thêm mới phiếu nhập
	 * @param {Object} body 
	 * @returns {Promise}
	 * CreatedBy: NTDUNG (30/09/2021)
	 */
	addVoucher(body) {
		let url = this.baseApiUrl + this.controller + '/add';
		return axios.put(url, body);
	}
	/**
	 * Cập nhật phiếu nhập
	 * @param {String} voucherId 
	 * @param {Object} body 
	 * @returns {Promise}
	 * CreatedBy: NTDUNG (30/09/2021)
	 */
	updateVoucher(voucherId, body) {
		let url = this.baseApiUrl + this.controller + `/update/${voucherId}`;
		return axios.put(url, body);
	}
}
export default new VoucherAPI();

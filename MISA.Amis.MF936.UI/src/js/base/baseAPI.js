import axios from "axios";
export default class BaseAPI {
	constructor(controller, field) {
		this.baseApiUrl = "https://localhost:44350/api/v1/";
		this.controller = controller;
		this.field = field;
	}

	/**
	 * Lấy tất cả thông tin
	 * @returns promise
	 * CreatedBy: NTDUNG (01/09/2021)
	 */
	getAll() {
		let api = this.baseApiUrl + this.controller;
		return axios.get(api);
	}

	/**
	 * Lấy theo id
	 * @param {string} id
	 * @returns promise
	 * CreatedBy: NTDUNG (01/09/2021)
	 */
	getById(id) {
		let api = this.baseApiUrl + this.controller + "/" + id;
		return axios.get(api);
	}

	/**
	 * thêm mới
	 * @param {object} body thông tin cần thêm
	 * @returns promise
	 * CreatedBy: NTDUNG (01/09/2021)
	 */
	async post(body) {
		let api = this.baseApiUrl + this.controller;
		return await axios.post(api, body);
	}

	/**
	 * Chỉnh sửa theo Id
	 * @param {string} id
	 * @param {object} body thông tin chỉnh sửa
	 * @returns promise
	 * CreatedBy: NTDUNG (01/09/2021)
	 */
	async put(id, body) {
		let api = this.baseApiUrl + this.controller + "/" + id;
		return await axios.put(api, body);
	}

	/**
	 * Lấy dữ liệu phân trang
	 * @param {String} filterString dữ liệu Lọc
	 * @param {Number} pageIndex trang hiện tại
	 * @param {Number} pageSize Kích thước - số bản ghi trên
	 * @returns {Promise}
	 * CreatedBy: NTDUNG (01/09/2021)
	 */
	getFilterPaging(filterString, pageIndex, pageSize) {
		var accountType;
		switch (this.field) {
			case "credit_account_id":
			case "warehouse_account_id":
				accountType = 6;
				break;
			case "debit_account_id":
				accountType = 1;
				break;
		}
		let api =
			this.baseApiUrl +
			this.controller +
			`/Filter?searchData=${filterString}&pageIndex=${pageIndex}&pageSize=${pageSize}`;
		if (accountType) api += `&type=${accountType}`;

		return axios.get(api);
	}

	/**
	 * Xóa nhiều theo Id
	 * @param {Array} listData mảng chứa các id
	 * @returns promise get từ call axios api
	 * CreatedBy: NTDUNG (01/09/2021)
	 */
	async deleteMany(body) {
		let api = this.baseApiUrl + this.controller;
		return await axios.delete(api, { data: body });
	}
	/**
	 * Xóa theo id
	 * @param {string} id  id của đối tượng
	 * @returns promise
	 * CreatedBy: NTDUNG (01/09/2021)
	 */
	delete(id) {
		let api = this.baseApiUrl + this.controller + "/" + id;
		return axios.delete(api);
	}

	/**
	 * Lấy mã mới
	 * @returns {Promise}
	 * CreatedBy: NTDUNG (07/10/2021)
	 */
	getNewCode() {
		let api = this.baseApiUrl + this.controller + "/NewCode";
		return axios.get(api);
	}
}

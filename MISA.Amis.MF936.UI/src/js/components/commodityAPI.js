import BaseAPI from "../base/baseAPI.js";
import axios from "axios";
class CommodityAPI extends BaseAPI {
	constructor() {
		super();
		this.controller = "Commoditys";
	}

    /**
     * Tạo hàng hoá mới
     * @returns {Promise}
     * CreatedBy: NTDUNG(01/10/2021)
     */
    async AddCommodity(body) {
        let url = this.baseApiUrl + this.controller + `/add`;
        return axios.post(url, body);
    }
	
}
export default new CommodityAPI();
import BaseAPI from "../base/baseAPI.js";
import axios from "axios";
class CommodityUnitAPI extends BaseAPI {
	constructor() {
		super();
		this.controller = "CommodityUnits";
	}

    /**
     * Lấy đơn vị tính cho hàng hoá
     * @param {String} commodityId 
     * @returns {Promise}
     * CreatedBy: NTDUNG(01/10/2021)
     */
    async GetUnits(commodityId) {
        let url = this.baseApiUrl + this.controller + `/units/${commodityId}`;
        return axios.get(url);
    }
	
}
export default new CommodityUnitAPI();
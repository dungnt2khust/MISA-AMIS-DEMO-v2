import axios from "axios";
import BaseAPI from "../base/baseAPI"
class CustomerAPI extends BaseAPI{
    constructor(){
        super();
        this.controller = "customers"
    }

    /**
     * lọc và phân trang nhân viên
     * @param {int} pageSize số bản ghi trên trang
     * @param {int} pageNum index trang 
     * @param {string} filterString
     * @returns promise
     * CreatedBy: NTDUNG (01/09/2021)
     */
    getCustomerFilter(pageSize, pageNum, filterString = ""){
        let api = this.baseApiUrl + this.controller;
        api += `/filter?pageSize=${pageSize}&pageNumber=${pageNum}&filterString=${filterString}`;
        return axios.get(api);
    }

    /**
     * Xuất dữ liệu ra file excel
     * @returns promise
     * CreatedBy: NTDUNG (01/09/2021)
     */
    export() {
        let api = this.baseApiUrl + this.controller + "/export";
        return axios.get(api, {responseType: "blob"});
    }
}
export default new CustomerAPI();
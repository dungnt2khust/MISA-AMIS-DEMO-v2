import moment from 'moment'

export default {
    methods: {
        /**
         * trả về date range tương ứng 
         * @param {string} dropdownData
         * @return {Array} YYYY-MM-DD
         * createdBy: NTDUNG (27/9/2021)
         */
        changeDatefromDropdown(dropdownData) {
            switch (dropdownData) {
                case "beginyeartocurr": {
                    let yearStart = moment().startOf('year').format('YYYY-MM-DD');
                    let currentDate = moment().format("YYYY-MM-DD");
                    return [yearStart, currentDate];
                }
                case "firsthalfyear": {
                    let yearStart = moment().startOf('year').format('YYYY-MM-DD');
                    let middleDate = moment("2021-06-01").endOf('month').format('YYYY-MM-DD');
                    return [yearStart, middleDate];
                }
                case "secondhalfyear": {
                    let yearEnd = moment().endOf('year').format('YYYY-MM-DD');
                    let middleDate = moment("2021-07-01").startOf('month').format('YYYY-MM-DD');
                    return [middleDate, yearEnd];
                }
                case "today": {
                    let currentDate = moment().format("YYYY-MM-DD");
                    return [currentDate, currentDate];
                }
                case "week": {
                    let currentDate = moment();
                    let weekStart = currentDate.clone().startOf('isoWeek').format("YYYY-MM-DD");
                    let weekEnd = currentDate.clone().endOf('isoWeek').format("YYYY-MM-DD");
                    return [weekStart, weekEnd];
                }
                case "month": {
                    let monthStart = moment().startOf('month').format('YYYY-MM-DD');
                    let endOfMonth = moment().endOf('month').format('YYYY-MM-DD');
                    return [monthStart, endOfMonth];
                }
                case "year":{
                    let yearStart = moment().startOf('year').format('YYYY-MM-DD');
                    let yearEnd = moment().endOf('year').format('YYYY-MM-DD');
                    return [yearStart, yearEnd];
                }
                case "jan":{
                    let monthStart = moment("2021-01-01").startOf('month').format('YYYY-MM-DD');
                    let endOfMonth = moment("2021-01-01").endOf('month').format('YYYY-MM-DD');
                    return [monthStart, endOfMonth];
                }
                case "feb":{
                    let monthStart = moment("2021-02-01").startOf('month').format('YYYY-MM-DD');
                    let endOfMonth = moment("2021-02-01").endOf('month').format('YYYY-MM-DD');
                    return [monthStart, endOfMonth];
                }
                case "mar":{
                    let monthStart = moment("2021-03-01").startOf('month').format('YYYY-MM-DD');
                    let endOfMonth = moment("2021-03-01").endOf('month').format('YYYY-MM-DD');
                    return [monthStart, endOfMonth];
                }
                case "apr":{
                    let monthStart = moment("2021-04-01").startOf('month').format('YYYY-MM-DD');
                    let endOfMonth = moment("2021-04-01").endOf('month').format('YYYY-MM-DD');
                    return [monthStart, endOfMonth];
                }
                case "may":{
                    let monthStart = moment("2021-05-01").startOf('month').format('YYYY-MM-DD');
                    let endOfMonth = moment("2021-05-01").endOf('month').format('YYYY-MM-DD');
                    return [monthStart, endOfMonth];
                }
                case "june":{
                    let monthStart = moment("2021-06-01").startOf('month').format('YYYY-MM-DD');
                    let endOfMonth = moment("2021-06-01").endOf('month').format('YYYY-MM-DD');
                    return [monthStart, endOfMonth];
                }
                case "july":{
                    let monthStart = moment("2021-07-01").startOf('month').format('YYYY-MM-DD');
                    let endOfMonth = moment("2021-07-01").endOf('month').format('YYYY-MM-DD');
                    return [monthStart, endOfMonth];
                }
                case "aug":{
                    let monthStart = moment("2021-08-01").startOf('month').format('YYYY-MM-DD');
                    let endOfMonth = moment("2021-08-01").endOf('month').format('YYYY-MM-DD');
                    return [monthStart, endOfMonth];
                }
                case "sep":{
                    let monthStart = moment("2021-09-01").startOf('month').format('YYYY-MM-DD');
                    let endOfMonth = moment("2021-09-01").endOf('month').format('YYYY-MM-DD');
                    return [monthStart, endOfMonth];
                }
                case "oct":{
                    let monthStart = moment("2021-10-01").startOf('month').format('YYYY-MM-DD');
                    let endOfMonth = moment("2021-10-01").endOf('month').format('YYYY-MM-DD');
                    return [monthStart, endOfMonth];
                }
                case "nov":{
                    let monthStart = moment("2021-11-01").startOf('month').format('YYYY-MM-DD');
                    let endOfMonth = moment("2021-11-01").endOf('month').format('YYYY-MM-DD');
                    return [monthStart, endOfMonth];
                }
                case "dec":{
                    let monthStart = moment("2021-12-01").startOf('month').format('YYYY-MM-DD');
                    let endOfMonth = moment("2021-12-01").endOf('month').format('YYYY-MM-DD');
                    return [monthStart, endOfMonth];
                }
            }
        }
    }
}
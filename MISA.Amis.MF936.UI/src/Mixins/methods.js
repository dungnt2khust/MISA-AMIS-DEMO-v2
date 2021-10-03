export default {
	methods: {
		/**
         * format string
		 * @param {ListString}
         * @returns {String}
         * CreatedBy: NTDUNG (03/09/2021)
         */
        formatString(targetString, ...strings) {
            for (let i = 0; i < strings.length; i++) {
                targetString = targetString.replace("{" + i + "}", strings[i]);
            }
            return targetString;
        },
		/**
		 * Tính toán vị trí hợp lý cho popup
		 * @param {DOMRect} button
		 * CreatedBy: NTDUNG (27/09/2021)
		 */
		positionPopup(button) {
			var position = {
				top: button.top + button.height + 10,
				left: button.left
			};

			return position;
		},
		/**
		 * So sánh 2 object
		 * @param {Object} object1
		 * @param {Object} object2
		 * CreatedBy: NTDUNG (03/09/2021)
		 */
		compareObjects(object1, object2) {
			// Mặc định là trùng
			var duplicate = true;

			// Duyệt từng thuộc tính của object 1
			// Khi có một giá trị khác thì break và gán là false
			for (var prop in object1) {
				if (prop.includes("Date")) {
					// Trường hợp là date

					// Khi cả 2 giá trị đều khách null
					if (object1[prop] && object2[prop]) {
						// Chỉ so sánh ngày tháng năm
						if (
							object1[prop].substring(0, 10) !== object2[prop].substring(0, 10)
						) {
							duplicate = false;
							break;
						}
						// Khi một trong 2 bằng null
					} else if (
						(!object1[prop] && object2[prop]) ||
						(object1[prop] && !object2[prop])
					) {
						duplicate = false;
						break;
					}
				} else {
					// Trường hợp bình thường

					// Khi cả 2 giá trị đều khách null
					if (object1[prop] && object2[prop]) {
						if (object1[prop] != object2[prop]) {
							duplicate = false;
							break;
						}
						// Khi một trong 2 bằng null
					} else if (
						(!object1[prop] && object2[prop]) ||
						(object1[prop] && !object2[prop])
					) {
						duplicate = false;
						break;
					}
				}
			}
			return duplicate;
		},
		/**
         * Kiểm tra phải object không
         * @param {object} object: object cần kiểm tra
         * CreatedBY: NTDUNG(01/10/2021) - Referenced
         */
        isObject(object) {
            return object != null && typeof object === 'object';
        },
		/**
		 * So sánh sâu 2 object
		 * @param {Object} object1 
		 * @param {Object} object2 
		 * @param {Array} escapeFields
		 * @returns {Boolean}
		 * CreatedBy: NTDUNG(01/10/2021) - Referenced
		 */
		deepEqualObject(object1, object2, escapeFields = []) {
			const keys1 = Object.keys(object1);
            const keys2 = Object.keys(object2);
            if (keys1.length !== keys2.length) {
                return false;
            }
            for (const key of keys1) {
                const val1 = object1[key];
                const val2 = object2[key];
                const areObjects = this.isObject(val1) && this.isObject(val2);
                if (
                    areObjects && !this.deepEqualObject(val1, val2, escapeFields) ||
                    !areObjects
                ) {
					if (escapeFields.indexOf(key) == -1) 
						if (key.includes('date')) {
							if (val1 && val2)
								if (val1.substring(0, 10) != val2.substring(0, 10)) {
									console.log(key)
									return false;
								}
							else if (val1 && !val2) 
								return false;
							else if (!val1 && val2)
								return false;
						}
						else {
							if (val1 != val2) {
								console.log(key)
								return false;
							}
						}
                }
            }
            return true;
		},
		/**
		 * Hàm tính ra element cha chứa lớp được truyền vào
		 * @param {Element} childE
		 * @param {String} parentClass
		 */
		findParentByClass(childE, parentClass) {
			var parentE = childE;
			if (parentE) {
				// Nếu không chứa class thì tiếp tục vòng lặp
				while (parentE.classList.contains(parentClass) == false) {
					// Đi ra một element cha
					parentE = parentE.parentElement;

					// Khi đã duyệt hết mà không có thì set null và thoát vòng lặp
					if (parentE.tagName == "BODY") {
						parentE = null;
						break;
					}
				}
			}
			// Trả về kết quả
			return parentE;
		},
		/**
		 * Format dữ liệu ngày tháng
		 * @param {string} value
		 * CreatedBy: NTDUNG (01/09/2021)
		 */
		formatDate(value) {
			if (value) {
				var currDate = new Date(value);
				var date = currDate.getDate();
				date = date < 10 ? "0" + date : date;
				var month = currDate.getMonth() + 1;
				month = month < 10 ? "0" + month : month;
				var year = currDate.getFullYear();

				return `${date}/${month}/${year}`;
			}
			return null;
		},
		/**
		 * Format tiền tệ
		 * @param {Number} value
		 * @returns {String}
		 */
		formatMoney(value) {
			if (value)
				return value.toFixed(1).replace(/\d(?=(\d{3})+\.)/g, "$&,");
			else {
				var zero = 0;
				return zero.toFixed(1).replace(/\d(?=(\d{3})+\.)/g, "$&,");
			}
		},
		/**
		 * Format chuỗi hiển thị trên input thành date
		 * @param {String} value
		 * @return {String}
		 */
		formatInputDate(value) {
			if (value.length == 10) {
				var date = value.substring(0, 2);
				var month = value.substring(3, 5);
				var year = value.substring(6, 10);
				return `${year}-${month}-${date}`;
			} else {
				return "";
			}
		},
		/**
		 * Hiển thị lỗi trả về từ server
		 * @param {Error} res
		 * CreatedBy: NTDUNG (04/09/2021)
		 */
		showError(res) {
			if (!res.response) {
				this.callDialog("error", "Có lỗi xảy ra. Vui lòng liên hệ MISA");
			} else {
				if (res.response.data) {
					// Show lỗi cho người dùng
					this.callDialog("warn", res.response.data.userMsg);
					// Show lỗi dev
					console.log({
						devMsg: res.response.data.devMsg,
						errorCode: res.response.data.errorCode,
						traceId: res.response.data.traceId,
					});
				} else {
					this.callDialog("error", res.response.data.Msg);
				}
			}
		},
		/**
		 * Chuyển chuỗi tiếng việt sang chuỗi bình thường
		 * @param {String} str
		 * @returns {String}
		 * CreatedBy: NTDUNG (05/09/2021) - Referenced
		 */
		removeVietnameseTones(str) {
			str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
			str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
			str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
			str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
			str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
			str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
			str = str.replace(/đ/g, "d");
			str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
			str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
			str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
			str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
			str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
			str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
			str = str.replace(/Đ/g, "D");
			// Some system encode vietnamese combining accent as individual utf-8 characters
			// Một vài bộ encode coi các dấu mũ, dấu chữ như một kí tự riêng biệt nên thêm hai dòng này
			str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ""); // ̀ ́ ̃ ̉ ̣  huyền, sắc, ngã, hỏi, nặng
			str = str.replace(/\u02C6|\u0306|\u031B/g, ""); // ˆ ̆ ̛  Â, Ê, Ă, Ơ, Ư
			// Remove extra spaces
			// Bỏ các khoảng trắng liền nhau
			str = str.replace(/ + /g, " ");
			str = str.trim();
			// Remove punctuations
			// Bỏ dấu câu, kí tự đặc biệt
			/* eslint-disable */
			str = str.replace(
				/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'|\"|\&|\#|\[|\]|~|\$|_|`|-|{|}|\||\\/g, // eslint-disable-line no-use-before-define
				" "
			);
			/* eslint-enable */
			return str;
		},
	},
};

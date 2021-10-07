export default {
	data() {
		return {
			keyPressTimeout: null,
			keyPressed: [],
		};
	},
	methods: {
		/**
		 * Lắng nghe sự kiện phím tắt
		 * @param {Array} shortKeysFunctions
		 * CreatedBy: NTDUNG(06/10/2021)
		 */
		shortKeysListeners(shortKeysFunctions) {
			return Object.assign({}, this.$listener, {
				keydown: (event) => {
                    console.log(event.key)
					shortKeysFunctions.forEach((item) => {
						switch (item["keys"].length) {
							case 1:
								this.executeOneKey(event, item["keys"], item["function"]);
								break;
							case 2:
								this.executeTwoKeys(event, item["keys"], item["function"]);
								break;
							case 3:
								this.executeThreeKeys(event, item["keys"], item["function"]);
								break;
						}
					});
				},
			});
		},
		/**
		 * Thực hiện phím tắt 1 phím
		 * @param {Event} event
		 * @param {Array} keys
		 * @param {Function} func
		 * CreatedBy: NTDUNG (06/10/2021)
		 */
		executeOneKey(event, keys, func) {
            var key = event.key;
			if (key.toLowerCase() == keys[0].toLowerCase()) {
				event.preventDefault();
				func();
			}
		},
		/**
		 * Thực hiện phím tắt 2 phím
		 * @param {Event} event
		 * @param {Array} keys
		 * @param {Function} func
		 * CreatedBy: NTDUNG (06/10/2021)
		 */
		executeTwoKeys(event, keys, func) {
            var key = event.key;
			// Khi phím đầu chưa được click và event gọi đển phím
			if (!this.keyPressed[0] && key.toLowerCase() == keys[0].toLowerCase()) {
				// Bỏ qua event mặc định
				event.preventDefault();
				// Đưa phím vào mảng và delay
				this.shortKeyDelay(keys[0], 500);
			} else {
				// Khi phím thứ 2 được xác nhân
				if (!this.keyPressed[1] && key.toLowerCase() == keys[1].toLowerCase()) {
					// Bỏ qua event mặc định
					event.preventDefault();
					// Thực thi
					func();
					// Clear toàn bộ phím
					this.keyPressed = [];
				}
			}
		},
		/**
		 * Thực hiện phím tắt 3 phím
		 * @param {Event} event
		 * @param {Array} keys
		 * @param {Function} func
		 * CreatedBy: NTDUNG (06/10/2021)
		 */
		executeThreeKeys(event, keys, func) {
            var key = event.key;
			// Khi phím đầu chưa được click và event gọi đển phím
			if (!this.keyPressed[0] && key.toLowerCase() == keys[0].toLowerCase()) {
				// Bỏ qua event mặc định
				event.preventDefault();
				// Đưa phím vào mảng và delay
				this.shortKeyDelay(keys[0], 500);
			} else {
				// Khi phím thứ 2 được xác nhân
				if (!this.keyPressed[1] && key.toLowerCase() == keys[1].toLowerCase()) {
					// Bỏ qua event mặc định
					event.preventDefault();
					// Đưa phím vào mảng và delay
					this.shortKeyDelay(keys[1], 500);
				} else {
					// Khi phím thứ 3 được xác nhân
					if (!this.keyPressed[2] && key.toLowerCase() == keys[2].toLowerCase()) {
						// Bỏ qua event mặc định
						event.preventDefault();
						// Thực thi
						func();
						// Clear toàn bộ phím
						this.keyPressed = [];
					}
				}
			}
		},
		/**
		 * Hàm xử lý delay phím tắt
		 * CreatedBy: NTDUNG (03/09/2021)
		 */
		shortKeyDelay(key, duration) {
			// Thêm phím mới vào mảng
			this.keyPressed.push(key);
			// Clear timeout
			clearTimeout(this.keyPressTimeout);
			// Đặt timeout
			this.keyPressTimeout = setTimeout(() => {
				this.keyPressed = [];
			}, duration);
		},
	}
};

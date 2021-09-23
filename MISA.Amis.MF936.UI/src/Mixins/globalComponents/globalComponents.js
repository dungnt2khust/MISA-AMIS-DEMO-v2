import methods from "../methods";

export default {
	mixins: [methods],
	data() {
		return {
			paginationHeight: 46,
			dialogAnswer: null,
			vm: this,
		};
	},
	methods: {
		/**
		 * Tạo listener cho context menu
		 * @param {Array} contextFunction
		 * @returns {Listener}
		 *  CreatedBy: NTDUNG (20/09/2021)
		 */
		contextMenuListeners(contextFunction) {
			var show = false;
			return Object.assign({}, this.$listener, {
				click: (event) => {
					if (!show)  {
						show = true;

						var element = this.findParentByClass(event.target, "context-menu");

						var elementRect = element.getBoundingClientRect();
						
						var elementPos = {
							top: elementRect.top,
							left: elementRect.left,
							width: element.offsetWidth,
							height: element.offsetHeight
						};
						this.$bus.$emit("showContextMenu", {
							contextFunction: contextFunction,
							pos: elementPos,
						});
					} else {
						show = false;
						this.$bus.$emit('hideContextMenu');
					}
					
				},
				focus: (event) => {
					var element = this.findParentByClass(event.target, 'context-menu');

					element.classList.add('context-menu--selected');
				},
				blur: (event) => {
					var element = this.findParentByClass(event.target, 'context-menu');

					element.classList.remove('context-menu--selected');

					this.$bus.$emit('hideContextMenu');
				}
			});
		},
		/**
		 * Lắng nghe sự kiện hiển thị và ẩn input (hover event)
		 * @param {string} message
		 * CreatedBy: NTDUNG (28/08/2021)
		 */
		tooltipListeners(message) {
			return Object.assign({}, this.$listener, {
				mouseenter: (event) => {
					// Truyền nội dung và vị trí cho tooltip
					var tooltipStyle = {
						message: message,
						top: event.clientY,
						left: event.clientX,
					};
					// Hiện tooltip
					this.$bus.$emit("showTooltip", tooltipStyle);
				},
				mouseleave: () => {
					// Ẩn tooltip
					this.$bus.$emit("hideTooltip");
				},
			});
		},
		/**
		 * Gọi đến dialog
		 * @param {string} type kiểu của dialog (ERROR, WARN)
		 * @param {string} message thông điệp muốn hiển thị
		 * CreatedBy: NTDUNG (31/08/2021)
		 */
		callDialog(type, message) {
			return new Promise((resolve, reject) => {
				// Set lại giá trị ban đầu cho dialog answer
				this.dialogAnswer = "";

				// Gọi đến sự kiện show dialog
				this.$bus.$emit("showDialog", { type: type, message: message });

				// Tạo sự kiện lắng nghe kết quả trả về của dialog
				this.$bus.$on("dialogConfirm", (data) => {
					// Huỷ sự kiện lắng nghe
					this.$bus.$off("dialogConfirm");

					// Gán kết quả
					this.dialogAnswer = data;
				});

				// Đợi trả về giá trị khi có câu trả lời
				let count = 0;
				let wait = setInterval(() => {
					count++;
					if (this.dialogAnswer !== "") {
						clearInterval(wait);
						resolve(this.dialogAnswer);
					}
					if (count >= 10000) {
						reject();
					}
				}, 20);
			});
		},
	},
};

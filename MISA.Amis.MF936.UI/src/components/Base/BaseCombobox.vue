<template lang="">
	<div
		class="combobox"
		:class="{
			'combobox--up': comboboxType == 'up',
			'combobox--show': comboboxState,
			'combobox--focus': focusState,
			'combobox--error': comboboxError,
		}"
	>
		<span v-if="label != ''" class="label">
			{{ label }} <span v-if="required" class="text-red">*</span>
		</span>
		<div v-on="comboboxListeners" class="combobox__main">
			<div class="combobox__value">
				<input
					v-on="inputListeners"
					type="text"
					class="combobox__input"
					:title="comboboxError ? errorMsg : ''"
					v-model="inputValue"
					:tabindex="tabIdx"
				/>
			</div>
			<div
				tabindex="0"
				@click="comboboxState = !comboboxState"
				class="combobox__button"
			>
				<div class="combobox__button-icon"></div>
			</div>
			<ul class="combobox__list">
				<li
					v-for="(item, index) in comboboxData"
					class="combobox__item"
					:class="{ 'combobox__item--selected': index == currIdx }"
					@click="comboboxItemOnClick(index)"
					:key="index"
					:index="index"
				>
					{{ item[field + "Name"] }}
				</li>
			</ul>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import methods from "../../mixins/methods.js";
	export default {
		name: "BaseComboboxBox",
		mixins: [methods],
		props: {
			comboboxType: {
				type: String,
				default: "down",
			},
			label: {
				type: String,
				default: "",
			},
			required: {
				type: Boolean,
				default: false,
			},
			comboboxData: {
				type: Array,
				default: function() {
					return [];
				},
			},
			field: {
				type: String,
				default: "",
			},
			value: {
				type: [String, Number],
				default: null,
			},
			passIndex: {
				type: Boolean,
				default: true,
			},
			formState: {
				type: Boolean,
				default: false,
			},
			validateState: {
				type: Boolean,
				default: false
			},
			tabIdx: {
				type: Number,
				default: 0
			}
		},
		data() {
			return {
				currIdx: null,
				comboboxState: false,
				comboboxError: false,
				focusState: false,
				inputValue: "",
				errorMsg: "",
				currLi: null,
				listElement: null
			};
		},
		computed: {
			/**
			 * Lắng nghe sự kiện trên combobox
			 * CreatedBy: NTDUNG ( 03/09/2021)
			 */
			comboboxListeners: function() {
				return Object.assign({}, this.$listener, {
					keydown: (event) => {
						if (
							event.key == "ArrowUp" ||
							event.key == "ArrowDown" ||
							event.key == "Enter"
						) {
							event.preventDefault();
							// Tìm ra li hiện tại
							this.currLi = this.$el.querySelector(".combobox__item--selected");
							var nextLi, prevLi;
							// Nếu phần tử hiện tại đang bị ẩn thì đưa về phần tử đầu tiên
							if (this.currLi === null || this.currLi.style.display == 'none'){
								this.listElement = this.$el.querySelectorAll('li');
								var findFirstLi;
								this.listElement.forEach((item, index) => {
									if (item.style.display == "block" && !findFirstLi) 
										findFirstLi = index;
								});
								if (findFirstLi !== undefined) {
									nextLi = this.listElement[findFirstLi];
								} else {
									nextLi = null;
								}
								prevLi = null;
							} else {
								nextLi = this.currLi.nextSibling;
								prevLi = this.currLi.previousSibling;
							}
							// Bắt sự kiện nút bấm
							switch (event.key) {
								case "ArrowUp":
									if (prevLi) {
										if (this.currLi)
											this.currLi.classList.remove("combobox__item--selected");
										prevLi.classList.add("combobox__item--selected");
										prevLi.scrollIntoView();
									}
									break;
								case "ArrowDown":
									if (nextLi) {
										if (this.currLi)
											this.currLi.classList.remove("combobox__item--selected");
										nextLi.classList.add("combobox__item--selected");
										nextLi.scrollIntoView();
									}
									break;
								case "Enter":
									var newIndex = this.currLi.getAttribute("index");
									if (this.comboboxState)
										this.comboboxItemOnClick(newIndex);
									else 
										this.comboboxState = true;
									break;
							}
						}
					},
				});
			},
			/**
			 * Lắng nghe sự kiện input
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			inputListeners: function() {
				return Object.assign({}, this.$listener, {
					input: () => {
						this.comboboxState = true;
						if (!this.inputValue) {
							this.comboboxError = true;
						} else {
							this.comboboxError = false
						}
						var inputValue = this.removeVietnameseTones(
							this.inputValue.toLowerCase().trim()
						);
						var allLiElements = this.$el.querySelectorAll("li");
						allLiElements.forEach((liElement) => {
							let liValue = this.removeVietnameseTones(
								liElement.innerText.toLowerCase().trim()
							);
							if (liValue.includes(inputValue)) {
								liElement.style.display = "block";
							} else {
								liElement.style.display = "none";
							}
						});
					},
					blur: () => {
						// this.$nextTick(() => {
						// 	if (
						// 		!this.findParentByClass(event.relatedTarget, "combobox__button")
						// 	)
						// 		this.comboboxState = false;
						// 	if (!this.comboboxState) {
						// 		this.checkValidCombobox();
						// 	}
						// 	this.focusState = false;
						// });	
						this.focusState = false;
					},
					focus: (event) => {
						if (!this.inputValue) {
							this.comboboxState = true;
						}
						event.target.select();
						this.focusState = true;
					},
				});
			},
		},
		methods: {
			/**
			 * Xử lý sự kiện click vào combobox item
			 * CreatedBy: NTDUNG (28/08/2021)
			 * @param {number} index chỉ số của item
			 */
			comboboxItemOnClick(index) {
				this.currIdx = index;
				this.comboboxState = false;
				this.comboboxError = false;
			},
			/**
			 * Đặt giá trị của input
			 * @param {Number} newIndex
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			setInputValue(newIndex) {
				if (newIndex == -1) {
					this.inputValue = '';
				} else {
					if (this.comboboxData[newIndex])
						this.inputValue = this.comboboxData[newIndex][this.field + "Name"];
					else this.inputValue = '';
				}
			},
			/**
			 * Xử lý value khi truyền vào
			 * @param {Number, String} value
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			resolveValue(value) {
				// Nếu null thì mặc định là 1
				if (value === null) this.currIdx = -1;
				else {
					// Nếu truyền index thì set index luôn
					if (this.passIndex) {
						this.currIdx = value;
					}
					// Ngược lại thì tìm ra index rồi gán
					else {
						var foundIdx = this.comboboxData.findIndex((item) => {
							return item[this.field + "Id"] == value;
						});
						this.currIdx = foundIdx;
					}
				}

				// Đặt giá trị mới cho input
				this.setInputValue(this.currIdx);
			},
			/**
			 * Kiểm tra combobox hợp lệ
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			checkValidCombobox() {
				this.$nextTick(() => {
					if (this.inputValue == '') {
						if (this.currIdx != -1) {
							this.setInputValue(this.currIdx);
							this.comboboxError = false;
						} else {
							this.comboboxError = true;
							this.errorMsg = this.$resourcesVN.RequireField.replace('@', this.label);
						}
					} else {
						var foundIdx = this.comboboxData.findIndex(item => {
							return item[this.field + "Name"].trim() == this.inputValue.trim();
						});
						if (foundIdx != -1) {
							this.setInputValue(foundIdx);
						} else {
							this.comboboxError = true;
							this.errorMsg = this.$resourcesVN.NotExist.replace('@', this.label);
							this.currIdx = -1;
						}
					}
				});
			}	
		},
		watch: {
			/**
			 * Khi form bật thì xoá lỗi cũ đi
			 * @param {Boolean} value
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			formState: function(value) {
				if (!value) {
					this.comboboxError = false;
					this.comboboxState = false;
				}
			},
			/**
			 * Khi value thay đổi thì thay đổi currIdx
			 * @param {Number, String} value
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			value: {
				handler(value) {
					this.resolveValue(value);
					if (value === null)
						// Đặt giá trị mới cho input
						this.setInputValue(value);
				},
				immediate: true,
			},
			/**
			 * Khi thay đổi currIdx thì cập nhật giá trị mới
			 * @param {number, string} value
			 * CreatedBy: NTDUNG (31/08/2021)
			 * ModifiedBy: NTUDNG (01/09/2021)
			 */
			currIdx: function(value) {
				// Nếu là index thì emit index
				if (this.passIndex) {
					this.$emit("input", value);
				}
				// Nếu là giá trị thì emit giá trị
				else {
					if (this.comboboxData[value])
						this.$emit("input", this.comboboxData[value][this.field + "Id"]);
				}
				if (this.currLi && this.currLi.getAttribute('index') != value) {
					this.currLi.classList.remove("combobox__item--selected");
				}
			},
			/**
			 * Khi input trống thì bảo lỗi nếu có required
			 * @param {String} value
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			inputValue: function(value) {
				if (!value && this.required && this.focusState) {
					this.comboboxError = true;
					this.errorMsg = this.$resourcesVN.RequireField.replace('@', this.label);
				}
			},
			/**
			 * Khi combobox tắt thì kiểm tra giá trị hợp lệ
			 * @param {Boolean} value
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			comboboxState: function(value) {
				if (!value) {
					this.checkValidCombobox();
				}
			},
			/**
			 * Khi validate thì show lỗi lại
			 * CreatedBy: NTDUNG (03/09/2021)
			 */
			validateState: function() {
				this.checkValidCombobox();
				if (this.comboboxError)
					this.$emit('inputError', this.errorMsg);
			}
		},
	};
</script>
<style>
	@import url('../../css/base/combobox.css');
</style>

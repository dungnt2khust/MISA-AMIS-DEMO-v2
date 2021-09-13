<template lang="">
	<div
		class="context-menu-container"
		:class="{ 'selectbox--show': contextMenuState, 'selectbox--up': isUp }"
		:style="setPositionOfContextMenu"
	>
		<ul ref="contextMenu" class="selectbox__list">
			<li
				v-for="(item, index) in contextMenuData"
				@click="choose(index)"
				class="selectbox__item"
				:key="index"
			>
				{{ item }}
			</li>
		</ul>
	</div>
</template>
<script>
	// LIBRARY
	import listeners from "../../Mixins/listeners.js";
	import baseAPI from "../../js/base/baseAPI.js";

	export default {
		name: "BaseContextMenu",
		mixins: [listeners],
		props: {
			contextMenuState: {
				type: Boolean,
				default: false,
			},
			contextMenuPosition: {
				type: Object,
				default: function() {
					return {};
				},
			},
			recordInfo: {
				type: Object,
				default: function() {
					return {};
				},
			},
			controller: {
				type: String,
				default: "",
			},
		},
		data() {
			return {
				contextMenuData: [this.$resourcesVN.Replication, this.$resourcesVN.Delete, this.$resourcesVN.StopUsing]
			};
		},
		computed: {
			/**
			 * Tính toàn vị trí của context menu
			 * CreatedBy: NTDUNG (28/08/2021)
			 */
			setPositionOfContextMenu() {
				// Tính toán vị trí của context menu
				var top = this.isUp
					? this.contextMenuPosition["top"] + "px"
					: this.contextMenuPosition["bottom"] + "px";
				return { top: top, left: this.contextMenuPosition["right"] + "px" };
			},
			/**
			 * Kiểm tra là up hay down
			 * CreatedBy: NTDUNG (30/08/2021)
			 */
			isUp() {
				// Vị trí bottom của nút bấm
				var btnBottom = this.contextMenuPosition["bottom"];
				// Chiều cao của context menu
				var contextMenuHeight = 90;
				// Chiều cao khả dụng của cửa sổ trình duyệt
				var windowHeight = window.innerHeight;

				return (
					btnBottom + contextMenuHeight + this.paginationHeight >= windowHeight
				);
			},
		},
		methods: {
			/**
			 * Xử lý khi chọn chức năng
			 * @param {Number} index
			 * CreatedBy: NTDUNG (02/09/2021)
			 */
			choose(index) {
				// Bỏ s ở controller
				var id = this.recordInfo.data[this.controller.slice(0, -1) + "Id"];
				var code = this.recordInfo.data[this.controller.slice(0, -1) + "Code"];
				var entityAPI = new baseAPI(this.controller);

				switch (index) {
					case 0:
						delete this.recordInfo.data[this.controller.slice(0, -1) + "Id"];
						this.$bus.$emit('showForm', {mode: 'CLONE', data: this.recordInfo.data});
						break;
					case 1:
						this.callDialog(
							"confirm",
							this.$resourcesVN.EmployeeDeleteMsg.replace('@', code)
						).then((answer) => {
							if (answer == "YES") {
								this.$bus.$emit("showLoading");
								entityAPI
									.delete(id)
									.then((res) => {
										this.$bus.$emit("showToastMessage", {message: this.$resourcesVN.DeleteSuccess, duration: 3000});
										this.$bus.$emit("reloadData");
										console.log(res);
									})
									.catch((res) => {
										this.callDialog("error", res.response.data.userMsg);
									});
							}	
						});
						break;
					case 2:
						this.callDialog('warn', this.$resourcesVN.FeatureNotAvaiable);
						break;
				}

				this.$emit("input", false);
			},
		},
	};
</script>
<style lang=""></style>

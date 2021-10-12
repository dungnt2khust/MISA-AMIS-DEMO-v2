<template lang="">
	<div class="formsmall__overlay">
		<!-- eslint-disable-->
		<div
			tabindex="0"
			v-on:keydown.ctrl.83.stop.prevent.exact="store"
			v-on:keydown.ctrl.shift.83.stop.prevent.exact="storeAndAdd"
			v-on:keydown.27.stop.prevent="hideForm"
			class="formsmall"
			v-on="formListeners"
			:style="positionOfForm"
		>
			<!-- eslint-enable-->
			<div class="formsmall__header">
				<h2 class="formsmall__header-title">{{ title }}</h2>
				<div class="formsmall__header-close">
					<base-help @click.native="help()" />
					<base-cancel @click.native="hideForm()" />
				</div>
			</div>
			<div class="formsmall__body">
				<slot name="body"></slot>
			</div>
			<div class="formsmall__footer fx-space-between">
				<base-button :method="hideForm" label="Huỷ" />
				<div class="fx">
					<base-button :method="store" label="Cất" class="mr-10" />
					<base-button :method="storeAndAdd" type="green" label="Cất và Thêm" />
				</div>
			</div>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import globalComponents from "../../../mixins/globalComponents/globalComponents.js";
	// COMPONENTS
	import BaseHelp from "./components/BaseHelp.vue";
	import BaseCancel from "./components/BaseCancel.vue";
	import BaseButton from "../Button/BaseButton.vue";

	export default {
		name: "BaseFormSmall",
		mixins: [globalComponents],
		components: {
			BaseHelp,
			BaseCancel,
			BaseButton,
		},
		props: {
			title: {
				type: String,
				default: "",
			},
			width: {
				type: String,
				default: "",
			},
		},
		data() {
			return {
				dragState: false,
				dragX: 0,
				dragY: 0,
				position: "unset",
			};
		},
		computed: {
			formListeners: function() {
				var dragXBegin, dragXEnd, dragYBegin, dragYEnd;
				return Object.assign({}, this.$listener, {
					// Nhấn xuống thì đặt vị trí bắt đầu và bật mode drag
					mousedown: (event) => {
						this.dragState = true;
						dragXBegin = event.clientX;
						dragYBegin = event.clientY;
					},
					// Tắt mode drag khi nhấc chuột
					mouseup: () => {
						this.dragState = false;
					},
					// Tắt mode drag khi ra khỏi form
					mouseleave: () => {
						this.dragState = false;
					},
					// Khi trong mode drag thì tìm ra offset và đặt position
					mousemove: (event) => {
						if (this.dragState) {
							// Gán vị trí mới
							dragXEnd = event.clientX;
							dragYEnd = event.clientY;
							this.dragY = dragYEnd - dragYBegin;
							this.dragX = dragXEnd - dragXBegin;
						}
					},
				});
			},
			/**
			 * Đặt vị trí cho form
			 * CreatedBy: NTDUNG (31/08/2021)
			 */
			positionOfForm() {
				return {
					width: this.width,
					"max-width": this.width,
					top: this.dragY == "50%" ? this.dragY : this.dragY + "px",
					left: this.dragX == "50%" ? this.dragX : this.dragX + "px",
				};
			},
		},
		methods: {
			/**
			 * Trợ giúp
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			help() {
				this.callDialog("warn", this.$resourcesVN.NOTIFY.FeatureNotAvaiable);
			},
			/**
			 * Ẩn form
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			hideForm() {
				this.$emit("hideForm");
			},
			/**
			 * Cất
			 * CreatedBy: NTDUNG (02/10/2021)
			 */
			store() {
				this.$emit("store");
			},
			/**
			 * Cất và thêm
			 * CreatedBy: NTDUNG (02/10/2021)
			 */
			storeAndAdd() {
				this.$emit("storeAndAdd");
			},
		},
	};
</script>
<style>
	@import url("../../../css/base/form/formsmall.css");
</style>

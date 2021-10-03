<template lang="">
	<div class="formsmall__overlay">
		<div
			class="formsmall"
			v-on="formListeners"
			:style="positionOfForm"
		>
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
				<slot name="footer"></slot>	
			</div>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import globalComponents from "../../../mixins/globalComponents/globalComponents.js"
	// COMPONENTS
	import BaseHelp from "./components/BaseHelp.vue";
	import BaseCancel from "./components/BaseCancel.vue";

	export default {
		name: "BaseFormSmall",
		mixins: [globalComponents],
		components: {
			BaseHelp,
			BaseCancel,
		},
		props: {
			title: {
				type: String,
				default: "",
			},
            width: {
                type: String,
                default: ''
            }
		},
		data() {
			return {
				dragState: false,
				dragX: 0,
				dragY: 0
			}
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
				return {width: this.width, 'max-width': this.width, top: this.dragY + "px", left: this.dragX + "px" };
			},
		},
		methods: {
			/**
			 * Trợ giúp
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			help() {
				this.callDialog('warn', this.$resourcesVN.NOTIFY.FeatureNotAvaiable);
			},
			/**
			 * Ẩn form
			 * CreatedBy: NTDUNG (29/09/2021)
			 */
			hideForm() {
				this.$emit('hideForm');
			}
		}
	};
</script>
<style>
	@import url("../../../css/base/form/formsmall.css");
</style>

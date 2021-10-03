<template lang="">
	<div v-show="formState" class="form">
		<div
			ref="form"
			v-on="formListeners"
			:style="positionOfForm"
			class="form__main"
		>
			<div class="form__header">
				<div class="form__header-content">
					<div class="form__title">{{title}}</div>
					<slot name="header"></slot>
				</div>
				<div class="form__action">
					<div @click="help()" class="form__help"></div>
					<div @click="cancel()" class="form__cancel"></div>
				</div>
			</div>
			<div class="form__body">	
				<slot name="body"></slot>
			</div>
			<div class="form__footer">
				<slot name="footer"></slot>
			</div>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import globalComponents from "../../../mixins/globalComponents/globalComponents.js";

	export default {
		name: "BaseForm",
		mixins: [globalComponents],
		props: {
			title: {
				type: String,
				default: ''
			}
		},
		data() {
			return {
				formState: true,
				dragState: false,
				dragX: 0,
				dragY: 0,
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
				return { top: this.dragY + "px", left: this.dragX + "px" };
			},
		},
		methods: {
			/**
			 * Trợ giúp
			 * CreatedBy: NTDUNG (02/10/2021)
			 */
			help() {
				this.callDialog(this.$enum.DIALOG_TYPE.Warn, this.$resourcesVN.NOTIFY.FeatureNotAvaiable);
			},
			/**
			 * Huỷ
			 * CreatedBy: NTDUNG (02/10/2021)
			 */
			cancel() {
				this.$emit('hideForm');
			}
		}
	};
</script>
<style>
	@import url('../../../css/base/form/form.css');
</style>

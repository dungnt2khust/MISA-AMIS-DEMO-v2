<template lang="">
	<div>
		<base-form-large
			v-model="formLargeState"
			:formLargeState="formLargeState"
		/>
	</div>
</template>
<script>
	// COMPONENTS
	import BaseFormLarge from "../../../Base/Form/BaseFormLarge.vue";

	export default {
		name: "WarehouseDetail",
		components: {
			BaseFormLarge,
		},
		data() {
			return {
				formLargeState: false,
			};
		},
		created() {
			this.$bus.$on("showWarehouseDetail", () => {
				this.formLargeState = true;
			});
			this.$bus.$on("hideWarehouseDetail", () => {
				this.formLargeState = false;
			});
		},
        destroyed() {
            this.$bus.$off('showWarehouseDetail');
            this.$bus.$off('hideWarehouseDetail');
        }
	};
</script>
<style lang=""></style>

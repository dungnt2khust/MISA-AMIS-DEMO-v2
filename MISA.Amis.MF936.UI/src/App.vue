<template>
	<div id="app">
		<the-menu :menuState="menuState" @toggleMenu="toggleMenu" />
		<div id="wrapper">
			<the-header :menuState="menuState" @toggleMenu="toggleMenu" />
			<router-view/>
		</div>
		<employee-detail/>
		<base-tooltip />
		<base-dialog />
		<the-loading />
		<base-toast-message/>
	</div>
</template>

<script>
	// LIBRARY
	// COMPONENT
	import TheMenu from "./components/Layout/TheMenu.vue";
	import TheHeader from "./components/Layout/TheHeader.vue";
	import BaseTooltip from "./components/Base/BaseTooltip.vue";
	import BaseDialog from "./components/Base/BaseDialog.vue";
	import TheLoading from "./components/Layout/TheLoading.vue";
	import EmployeeDetail from "./components/views/employee/EmployeeDetail.vue";
	import BaseToastMessage from "./components/Base/BaseToastMessage.vue"

	export default {
		name: "App",
		components: {
			EmployeeDetail,
			TheMenu,
			TheHeader,
			TheLoading,
			BaseTooltip,
			BaseDialog,
			BaseToastMessage
		},
		data() {
			return {
				menuState: true,
				reloadTable: false,
			};
		},	
		methods: {
			/**
			 * Toggle menu
			 * CreatedBy: NTDUNG (28/08/2021)
			 * ModifiedBy: NTDUNG (28/08/2021)
			 */
			toggleMenu() {
				this.menuState = !this.menuState;
			} 
		},
	};
</script>
<style>
	@import url("./css/main.css");
</style>

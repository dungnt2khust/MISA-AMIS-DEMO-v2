<template lang="">
	<div id="content">
		<base-tab :currTab="currTab" :listTab="listTab" v-model="currTab" />
		<router-view/>
		<warehouse-detail/>
	</div>
</template>
<script>
	// COMPONENTS
	import BaseTab from "../../Base/Tab/BaseTab.vue";
	import WarehouseDetail from "./tabs/WarehouseDetail.vue";

	export default {
		name: "WarehousePage",
		components: {
			BaseTab,
			WarehouseDetail,
		},
		data() {
			return {
				currTab: 0,
				listTab: [
					{
						name: this.$resourcesVN.WAREHOUSE_TABS.Process,
						path: "/warehouse/process",
					},
					{
						name: this.$resourcesVN.WAREHOUSE_TABS.InwardOutwardList,
						path: "/warehouse/InwardOutwardList",
					},
					{
						name: this.$resourcesVN.WAREHOUSE_TABS.Report,
						path: "/warehouse/report",
					},
				],
			};
		}	
	};
</script>
<style>
	@import url("../../../css/views/warehouse/warehouse.css");
</style>

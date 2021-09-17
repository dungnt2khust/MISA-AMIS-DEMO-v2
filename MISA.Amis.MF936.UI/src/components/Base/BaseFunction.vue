<template lang="">
	<div class="funtion">
		<div class="function__control-query">
			<div class="function__search">
				<input
					ref="inputSearch"
					type="text"
					class="function__search-input"
					:placeholder="$resourcesVN.FUNCTION.SearchByCodeNamePhone"
				/>
			</div>
			<div
				class="function__reload"
				v-on="tooltipListeners($resourcesVN.FUNCTION.ReloadData)"
			>
				<div class="function__reload-icon"></div>
			</div>
			<div
				class="function__export"
				v-on="tooltipListeners($resourcesVN.FUNCTION.ExportE)"
			>
				<div class="function__export-icon"></div>
			</div>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import globalComponents from '../../mixins/globalComponents/globalComponents.js'

	export default {
		name: "BaseFunction",
		mixins: [globalComponents]
	};
</script>
<style>
	@import url('../../css/base/function.css');
</style>

<template lang="">
	<div id="warehouse">
		<div class="warehouse__process">
			<div class="warehouse__tutorial">
				<div class="warehouse__tutorial-title">
					{{ $resourcesVN.WAREHOUSE_TUTORIAL_TITLE }}
				</div>
				<div class="warehouse__tutorial-map">
					<div class="warehouse__node">
						{{ $resourcesVN.WAREHOUSE_MAP_LABELS.ProductionOrder }}
					</div>
					<div class="warehouse__node">
						{{ $resourcesVN.WAREHOUSE_MAP_LABELS.AssembleDismantlingOrder }}
					</div>
					<div class="warehouse__node">
						{{ $resourcesVN.WAREHOUSE_MAP_LABELS.ExportInventory }}
					</div>
					<div @click="importInventory()" class="warehouse__node">
						{{ $resourcesVN.WAREHOUSE_MAP_LABELS.ImportInventory }}
					</div>
					<div class="warehouse__node">
						{{ $resourcesVN.WAREHOUSE_MAP_LABELS.Transfer }}
					</div>
					<div class="warehouse__node">
						{{ $resourcesVN.WAREHOUSE_MAP_LABELS.OutwardPrice }}
					</div>
					<div class="warehouse__node">
						{{ $resourcesVN.WAREHOUSE_MAP_LABELS.Audit }}
					</div>
				</div>
				<div class="warehouse__tutorial-function">
					<div
						v-for="(item, index) in WarehouseProcessFunction"
						@click="processFunction(index)"
						class="warehouse__tutorial-function-item"
						:key="index"
					>
						<div
							class="warehouse__tutorial-function-icon"
							:style="{
								'background-position': item['position'],
								width: item['width'],
								height: item['height'],
							}"
						></div>
						<div class="warehouse__tutorial-function-label">
							{{ item["name"] }}
						</div>
					</div>
				</div>
			</div>
			<div class="warehouse__report">
				<div class="warehouse__report-title">
					{{ $resourcesVN.WAREHOUSE_REPORT.Title }}
				</div>
				<ul class="warehouse__report-list">
					<li class="warehouse__report-item">Sổ chi tiết vật tư hàng hoá</li>
					<li class="warehouse__report-item">Tổng hợp tồn kho</li>
					<li class="warehouse__report-item">
						Tổng hợp tồn kho theo nhiều đơn vị tính
					</li>
					<li class="warehouse__report-item">
						Tổng hợp tồn trên nhiều kho (Dạng bảng chéo)
					</li>
					<li class="warehouse__report-item">
						Tổng hợp nhập xuất tồn trên nhiều kho
					</li>
				</ul>
				<div class="warehouse__report-all">
					{{ $resourcesVN.WAREHOUSE_REPORT.All }}
				</div>
			</div>
		</div>
	</div>
</template>
<script>
	// LIBRARY
	import WarehouseProcessFunction from "../../../../mixins/warehouse/warehouseProcessFunction";

	export default {
		name: "WarehouseProcess",
		mixins: [WarehouseProcessFunction],
		methods: {
			/**
			 * Dẫn đến nhập kho
			 * CreatedBy: NTDUNG (02/10/2021)
			 */
			importInventory() {
				this.$router.push({
					path: "InwardOutwardList/Add"
				});
			},
			/**
			 * Đường dẫn chức năng
			 * @param {Number} index
			 * CreatedBy: NTDUNG (01/10/2021)
			 */
			processFunction(index) {
				if (index == 0) {
					this.$router.push({
						path: "InwardOutwardList/Normal"
					});
				}
			}
		},
	};
</script>
<style>
	@import url("../../../../css/views/warehouse/warehouseprocess.css");
</style>

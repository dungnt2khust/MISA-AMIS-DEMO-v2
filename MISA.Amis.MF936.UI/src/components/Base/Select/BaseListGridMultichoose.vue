<template lang="">
	<div class="listgrid">
		<thead class="listgrid__header">
			<tr>
				<th
					v-for="(item, index) in listGridStyle"
					:style="{
						'max-width': item['width'],
						'text-transform': 'none',
					}"
					:key="index"
				>
					{{ item["name"] }}
				</th>
				<th></th>
			</tr>
		</thead>
		<tbody class="listgrid__body" :style="{ 'max-height': '160px' }">
			<tr
				@click="chooseOption(itemData)"
				v-for="(itemData, indexData) in listGridData"
				:class="{
					'listgrid--multi-selected': selectedOption(itemData),
				}"
				:key="indexData"
			>
				<td
					v-for="(itemStyle, indexStyle) in listGridStyle"
					:style="{ 'min-width': itemStyle['width'] }"
					:key="indexStyle"
				>
					{{ itemData[itemStyle["field"]] }}
				</td>
				<td>
					<div
						v-if="
							listSelected.findIndex((item) => {
								return item[vmodelField] == itemData[vmodelField];
							}) != -1
						"
						class="listgrid__checked"
					></div>
				</td>
			</tr>
		</tbody>
		<div v-if="!listGridData.length" class="listgrid__nodata">
			{{ $resourcesVN.NOTIFY.Nodata}}
		</div>
	</div>
</template>
<script>
	export default {
		name: "BaseListGrid",
		props: {
			listSelected: {
				type: Array,
				default: null,
			},
			listGridStyle: {
				type: Array,
				default: function() {
					return [];
				},
			},
			listGridData: {
				type: Array,
				default: function() {},
			},
			vmodelField: {
				type: String, 
				default: ""
			}
		},
		data() {
			return {};
		},
		methods: {
			/**
			 * Chọn một item trong list
			 * @param {Object} itemData
			 * CreatedBy: NTDUNG (26/09/2021)
			 */
			chooseOption(itemData) {
				var listSelected = this.listSelected;
				var foundIdx = listSelected.findIndex((item) => {
					return item[this.vmodelField] == itemData[this.vmodelField];
				});
				if (foundIdx == -1) {
					listSelected.push(itemData);
				} else {
					listSelected.splice(foundIdx, 1);
				}
				this.$emit("input", listSelected);
				this.$emit("hideGrid");
			},
			/**
			 * Kiểm tra đâu là lựa chọn đang được chọn
			 * @param {Object} itemData
			 * CreatedBy: NTDUNG (26/09/2021)
			 */
			selectedOption(itemData) {
				if (this.type == "multichoose") {
					var foundIdx = this.listSelected.findIndex((item) => {
						return item[this.vmodelField] == itemData[this.vmodelField];
					});
					if (foundIdx != -1) return true;
					else return false;
				}
			},
		},
	};
</script>
<style>
	@import url("../../../css/base/select/listgrid.css");
</style>

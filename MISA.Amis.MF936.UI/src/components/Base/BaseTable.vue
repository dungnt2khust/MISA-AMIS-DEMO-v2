<template lang="">
	<table class="table">
		<thead>
			<tr>
				<th>
					<base-checkbox />
				</th>
				<th v-for="(item, index) in tableStyle" :key="index">
					{{ item["name"] }}
				</th>
				<th>{{ $resourcesVN.FUNCTION.TableFunction }}</th>
			</tr>
		</thead>
		<tbody>
			<tr v-for="(itemData, indexData) in tableData" :key="indexData">
				<td>
					<base-checkbox/>
				</td>
				<td
					v-for="(itemStyle, indexStyle) in tableStyle"
					:class="recordStyle(itemStyle)"
					:key="indexStyle"
				>
					{{ tdValue(itemData, itemStyle) }}
				</td>
				<td>
					<div
						class="table__function"
						:class="{ 'table--loading': tableLoading }"
					>
						<div class="table__update">
							{{ $resourcesVN.TABLE.Watch}}
						</div>
						<div tabindex="1" class="context-menu">
							<div class="context-menu__icon"></div>
						</div>
					</div>
				</td>
			</tr>
		</tbody>
		<base-context-menu
			:contextMenuState="contextMenuState"
			v-model="contextMenuState"
		/>
	</table>
</template>
<script>
	// LIBRARY
	import Table from "../../mixins/table.js";
	import methods from "../../mixins/methods";

	// COMPONENTS
	import BaseContextMenu from "./BaseContextMenu.vue";
	import BaseCheckbox from "./BaseCheckbox.vue";

	export default {
		name: "BaseTableOld",
		mixins: [Table, methods],
		components: {
			BaseContextMenu,
			BaseCheckbox,
		},
		props: {
			tableStyle: {
				type: Array,
				default: function() {
					return [];
				},
			},
			tableData: {
				type: Array,
				default: function() {
					return [];
				},
			},
			tableLoading: {
				type: Boolean,
				default: false,
			},
		},
		data() {
			return {
				contextMenuState: false,
			};
		},
		computed: {},
		methods: {
			/**
			 * Đặt kiểu cho ô dữ liệu
			 * @param {Object} itemStyle kiểu được lưu trong object
			 * CreatedBy: NTDUNG (17/09/2021)
			 */
			recordStyle(itemStyle) {
				var styleClass = {};
				// Kiểu text đặc biệt
				switch (itemStyle["type"]) {
					case 2:
						styleClass["text-blue"] = true;
						break;
				}
				// Vị trí cho dữ liệu trong ô
				switch (itemStyle["pos"]) {
					case 0:
						styleClass["text-align-left"] = true;
						break;
					case 1:
						styleClass["text-align-center"] = true;
						break;
					case 2:
						styleClass["text-align-right"] = true;
						break;
				}
				return styleClass;
			},
		},
	};
</script>
<style>
	@import url("../../css/base/table.css");
</style>

<template lang="">
	<div class="commodity">
		<div class="fx-wrap mt-20">
			<base-input
				label="Tên"
				:value="data['dataMaster']['commodity_name']"
				v-model="data['dataMaster']['commodity_name']"
				:required="true"
				class="fx-1"
			/>
			<base-input
				label="Mã"
				:value="data['dataMaster']['commodity_code']"
				v-model="data['dataMaster']['commodity_code']"
				:required="true"
				class="fx-1/5 mt-8"
			/>
			<base-combobox-multi-choose
				label="Nhóm VTHH"
				class="fx-4/5 mt-8"
				:api="WAREHOUSE_TABLE.InWardDetail.COMMODITY_GROUP['api']"
				:controller="WAREHOUSE_TABLE.InWardDetail.COMMODITY_GROUP['controller']"
				v-model="
					data['dataMaster'][
						WAREHOUSE_TABLE.InWardDetail.COMMODITY_GROUP['vmodel']
					]
				"
				:valueBind="
					data['dataMaster'][
						WAREHOUSE_TABLE.InWardDetail.COMMODITY_GROUP['vmodel']
					]
				"
				:vmodelField="WAREHOUSE_TABLE.InWardDetail.COMMODITY_GROUP['vmodel']"
				:field="WAREHOUSE_TABLE.InWardDetail.COMMODITY_GROUP['field']"
				:default="
					data['dataMaster'][
						WAREHOUSE_TABLE.InWardDetail.COMMODITY_GROUP['field']
					]
				"
				:subfield="WAREHOUSE_TABLE.InWardDetail.COMMODITY_GROUP['subfield']"
				:display="WAREHOUSE_TABLE.InWardDetail.COMMODITY_GROUP['display']"
				:listGridStyle="WAREHOUSE_TABLE.InWardDetail.COMMODITY_GROUP['style']"
				:form="WAREHOUSE_TABLE.InWardDetail.COMMODITY_GROUP['form']"
			/>
			<base-combobox-advance
				label="Đơn vị tính chính"
				class="fx-1 mt-8"
				width="200px"
				:api="WAREHOUSE_TABLE.InWardDetail.UNIT['api']"
				:controller="WAREHOUSE_TABLE.InWardDetail.UNIT['controller']"
				v-model="
					data['dataMaster'][WAREHOUSE_TABLE.InWardDetail.UNIT['vmodel']]
				"
				:valueBind="
					data['dataMaster'][WAREHOUSE_TABLE.InWardDetail.UNIT['vmodel']]
				"
				:vmodelField="WAREHOUSE_TABLE.InWardDetail.UNIT['vmodel']"
				:field="WAREHOUSE_TABLE.InWardDetail.UNIT['field']"
				:default="
					data['dataMaster'][WAREHOUSE_TABLE.InWardDetail.UNIT['field']]
				"
				:subfield="WAREHOUSE_TABLE.InWardDetail.UNIT['subfield']"
				:display="WAREHOUSE_TABLE.InWardDetail.UNIT['display']"
				:listGridStyle="WAREHOUSE_TABLE.InWardDetail.UNIT['style']"
				:form="WAREHOUSE_TABLE.InWardDetail.UNIT['form']"
				:syncfield="WAREHOUSE_TABLE.InWardDetail.UNIT['syncfield']"
			/>
			<base-text-area label="Mô tả" class="fx-1 mt-8" />
		</div>
		<base-form-partition label="Thông tin ngầm định">
			<template v-slot:body>
				<div class="mt-20">
					<div class="fx">
						<base-combobox-advance
							label="Kho ngầm định"
							class="pr-26"
							width="150px"
							:api="WAREHOUSE_TABLE.InWardDetail.WAREHOUSE['api']"
							:controller="WAREHOUSE_TABLE.InWardDetail.WAREHOUSE['controller']"
							v-model="
								data['dataMaster'][
									WAREHOUSE_TABLE.InWardDetail.WAREHOUSE['vmodel']
								]
							"
							:valueBind="
								data['dataMaster'][
									WAREHOUSE_TABLE.InWardDetail.WAREHOUSE['vmodel']
								]
							"
							:vmodelField="WAREHOUSE_TABLE.InWardDetail.WAREHOUSE['vmodel']"
							:field="WAREHOUSE_TABLE.InWardDetail.WAREHOUSE['field']"
							:default="
								data['dataMaster'][
									WAREHOUSE_TABLE.InWardDetail.WAREHOUSE['field']
								]
							"
							:subfield="WAREHOUSE_TABLE.InWardDetail.WAREHOUSE['subfield']"
							:display="WAREHOUSE_TABLE.InWardDetail.WAREHOUSE['display']"
							:listGridStyle="WAREHOUSE_TABLE.InWardDetail.WAREHOUSE['style']"
							:form="WAREHOUSE_TABLE.InWardDetail.WAREHOUSE['form']"
						/>
						<base-combobox-advance
							label="TK kho"
							class="pr-26"
							type="small"
							width="150px"
							:api="WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['api']"
							:controller="
								WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['controller']
							"
							v-model="
								data['dataMaster'][
									WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['vmodel']
								]
							"
							:valueBind="
								data['dataMaster'][
									WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['vmodel']
								]
							"
							:vmodelField="
								WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['vmodel']
							"
							:field="WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['field']"
							:default="
								data['dataMaster'][
									WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['field']
								]
							"
							:subfield="
								WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['subfield']
							"
							:display="
								WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['display']
							"
							:listGridStyle="
								WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['style']
							"
							:form="WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['form']"
						/>
						<base-input
							label="Đơn giá mua gần nhất"
							width="155px"
							:pos="$enum.POS.Right"
						/>
					</div>
					<div class="fx">
						<base-combobox-advance
							label="TK doanh thu"
							class="pr-26 mt-16"
							width="150px"
							type="small"
							:api="WAREHOUSE_TABLE.InWardDetail.REVENUE_ACCOUNT['api']"
							:controller="
								WAREHOUSE_TABLE.InWardDetail.REVENUE_ACCOUNT['controller']
							"
							v-model="
								data['dataMaster'][
									WAREHOUSE_TABLE.InWardDetail.REVENUE_ACCOUNT['vmodel']
								]
							"
							:valueBind="
								data['dataMaster'][
									WAREHOUSE_TABLE.InWardDetail.REVENUE_ACCOUNT['vmodel']
								]
							"
							:vmodelField="
								WAREHOUSE_TABLE.InWardDetail.REVENUE_ACCOUNT['vmodel']
							"
							:field="WAREHOUSE_TABLE.InWardDetail.REVENUE_ACCOUNT['field']"
							:default="
								data['dataMaster'][
									WAREHOUSE_TABLE.InWardDetail.REVENUE_ACCOUNT['field']
								]
							"
							:subfield="
								WAREHOUSE_TABLE.InWardDetail.REVENUE_ACCOUNT['subfield']
							"
							:display="WAREHOUSE_TABLE.InWardDetail.REVENUE_ACCOUNT['display']"
							:listGridStyle="
								WAREHOUSE_TABLE.InWardDetail.REVENUE_ACCOUNT['style']
							"
							:form="WAREHOUSE_TABLE.InWardDetail.REVENUE_ACCOUNT['form']"
						/>
						<base-combobox-advance
							label="TK chi phí"
							class="pr-26 mt-16"
							width="150px"
							type="small"
							:api="WAREHOUSE_TABLE.InWardDetail.COST_ACCOUNT['api']"
							:controller="
								WAREHOUSE_TABLE.InWardDetail.COST_ACCOUNT['controller']
							"
							v-model="
								data['dataMaster'][
									WAREHOUSE_TABLE.InWardDetail.COST_ACCOUNT['vmodel']
								]
							"
							:valueBind="
								data['dataMaster'][
									WAREHOUSE_TABLE.InWardDetail.COST_ACCOUNT['vmodel']
								]
							"
							:vmodelField="WAREHOUSE_TABLE.InWardDetail.COST_ACCOUNT['vmodel']"
							:field="WAREHOUSE_TABLE.InWardDetail.COST_ACCOUNT['field']"
							:default="
								data['dataMaster'][
									WAREHOUSE_TABLE.InWardDetail.COST_ACCOUNT['field']
								]
							"
							:subfield="WAREHOUSE_TABLE.InWardDetail.COST_ACCOUNT['subfield']"
							:display="WAREHOUSE_TABLE.InWardDetail.COST_ACCOUNT['display']"
							:listGridStyle="
								WAREHOUSE_TABLE.InWardDetail.COST_ACCOUNT['style']
							"
							:form="WAREHOUSE_TABLE.InWardDetail.COST_ACCOUNT['form']"
						/>
					</div>
				</div>
			</template>
		</base-form-partition>

		<base-form-partition label="Đơn vị chuyển đổi">
			<template v-slot:body>
				<base-form-info
					class="mt-8"
					content="
                        Trường hợp vật tư, hàng hóa sử dụng đơn vị tính khác nhau khi nhập/xuất thì khai báo thêm các đơn vị tính ở đây.
                        VD: 1 tạ = 100kg, 1 két bia = 20 chai , ..."
				/>
				<base-table-input
					class="mt-20"
					:defaultBind="{
						name: 'commodity_id',
						value: data['dataMaster']['commodity_id'],
					}"
					:tableStyle="WAREHOUSE_TABLE.InWardDetail.CONVERSION_UNIT"
					:tableData="data['dataDetail']"
					v-model="data['dataDetail']"
				/>
			</template>
		</base-form-partition>
	</div>
</template>
<script>
	// LIBRARY
	import warehouseTable from "../../../../mixins/warehouse/warehouseTable";
	// COMPONENTS
	import BaseFormPartition from "../../../Base/Form/components/BaseFormPartition.vue";
	import BaseInput from "../../../Base/BaseInput.vue";
	import BaseComboboxAdvance from "../../../Base/Select/BaseComboboxAdvance.vue";
	import BaseComboboxMultiChoose from "../../../Base/Select/BaseComboboxMultiChoose.vue";
	import BaseFormInfo from "../../../Base/Form/components/BaseFormInfo.vue";
	import BaseTextArea from "../../../Base/Input/BaseTextArea.vue";
	import BaseTableInput from "../../../Base/Table/BaseTableInput.vue";

	export default {
		name: "WarehouseCommodity",
		mixins: [warehouseTable],
		components: {
			BaseFormPartition,
			BaseInput,
			BaseComboboxAdvance,
			BaseComboboxMultiChoose,
			BaseFormInfo,
			BaseTextArea,
			BaseTableInput,
		},
		props: {
			data: {
				type: Object,
				default: function() {
					return {};
				},
			},
		},
		created() {
			this.$bus.$on("changeConversionUnitDetail", (index, newId, data) => {
				if (newId == "INPUT") {
					// Id của detail
					var newIdFound = this.data["dataDetail"][index]["unit_id"];
					if (this.conversionUnit.length) {
						// Item detail
						let newItemDetail = this.conversionUnit.find((item) => {
							return item["unit_id"] == newIdFound;
						});
						// Item master
						let newItemMaster = this.conversionUnit.find((item) => {
							return item["unit_id"] == this.data["dataMaster"]["unit_id"];
						});
						let newRate = data;
						if (newItemMaster && newItemDetail) {
							let newDescription = `1 ${newItemMaster["unit_name"]} = ${newRate} ${newItemDetail["unit_name"]}`;
							this.$set(
								this.data["dataDetail"][index],
								"description",
								newDescription
							);
						}
					}
				} else {
					this.conversionUnit = data;
					// Item detail
					let newItemDetail = this.conversionUnit.find((item) => {
						return item["unit_id"] == newId;
					});
					// Item master
					let newItemMaster = this.conversionUnit.find((item) => {
						return item["unit_id"] == this.data["dataMaster"]["unit_id"];
					});
					let newRate = this.data["dataDetail"][index]["rate"];
					if (newRate && newItemMaster) {
						let newDescription = `1 ${newItemMaster["unit_name"]} = ${newRate} ${newItemDetail["unit_name"]}`;
						this.$set(
							this.data["dataDetail"][index],
							"description",
							newDescription
						);
					}
				}
			});
			this.$bus.$on("changeConversionUnitMaster", (indexFail, newId) => {
				// Item master
				let newItemMaster;
				if (this.conversionUnit.length) {
					newItemMaster = this.conversionUnit.find((item) => {
						return item["unit_id"] == newId;
					});
					this.data["dataDetail"].forEach((item, index) => {
						// Item detail
						if (item) {
							let newItemDetail = this.conversionUnit.find((subItem) => {
								return item["unit_id"] == subItem["unit_id"];
							});
							let newDescription = `1 ${newItemMaster["unit_name"]} = ${item["rate"]} ${newItemDetail["unit_name"]}`;
							console.log(newDescription);
							this.$set(
								this.data["dataDetail"][index],
								"description",
								newDescription
							);
						}
					});
				}
			});
		},
		data() {
			return {
				conversionUnit: [],
			};
		},
		methods: {},
	};
</script>
<style></style>

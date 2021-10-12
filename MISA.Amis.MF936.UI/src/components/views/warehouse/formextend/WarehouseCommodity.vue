<template lang="">
	<div class="commodity">
		<div class="fx-wrap mt-20">
			<base-input
				label="Tên"
				:value="data['dataMaster']['commodity_name']"
				v-model="data['dataMaster']['commodity_name']"
				:required="true"
				:showRequired="true"
				:formName="formName"
				class="fx-1"
			/>
			<base-input
				label="Mã"
				:value="data['dataMaster']['commodity_code']"
				v-model="data['dataMaster']['commodity_code']"
				:showRequired="true"
				:required="true"
				:formName="formName"
				class="fx-1/5 mt-8"
			/>
			<base-combobox-multi-choose
				label="Nhóm VTHH"
				class="fx-4/5 mt-8"
				:controller="WAREHOUSE_TABLE.InWardDetail.COMMODITY_GROUP['controller']"
				v-model="
					data['dataMaster'][
						WAREHOUSE_TABLE.InWardDetail.COMMODITY_GROUP['vmodel']
					]
				"
				:value="
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
				:defaultIndex="currIdx"
			/>
			<base-combobox-advance
				label="Đơn vị tính chính"
				class="fx-1/4 mt-8"
				:controller="WAREHOUSE_TABLE.InWardDetail.UNIT['controller']"
				v-model="
					data['dataMaster'][WAREHOUSE_TABLE.InWardDetail.UNIT['vmodel']]
				"
				:value="data['dataMaster'][WAREHOUSE_TABLE.InWardDetail.UNIT['vmodel']]"
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
				:required="true"
				:showRequired="true"
				:formName="formName"
			/>

			<div class="fx-1 mt-20">
				<div class="fx-nm-wrap pt-20 border-top">
					<base-input
						label="Số lượng"
						v-model="data['dataMaster']['quantity']"
						:value="data['dataMaster']['quantity']"
						class="fx-np-1/4 pr-8"
						:pos="$enum.POS.Right"
						:required="true"
						:formName="formName"
						format="money"
					/>
					<base-text-area
						label="Mô tả"
						v-model="data['dataMaster']['description']"
						:value="data['dataMaster']['description']"
						class="fx-np-1 mt-8"
					/>
				</div>
			</div>
		</div>
		<base-form-partition label="Thông tin ngầm định">
			<template v-slot:body>
				<div class="mt-20">
					<div class="fx">
						<base-combobox-advance
							label="Kho ngầm định"
							class="pr-26"
							width="150px"
							:controller="WAREHOUSE_TABLE.InWardDetail.WAREHOUSE['controller']"
							v-model="
								data['dataMaster'][
									WAREHOUSE_TABLE.InWardDetail.WAREHOUSE['vmodel']
								]
							"
							:value="
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
							:formName="formName"
						/>
						<base-combobox-advance
							label="TK kho"
							class="pr-26"
							type="small"
							width="150px"
							:controller="
								WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['controller']
							"
							v-model="
								data['dataMaster'][
									WAREHOUSE_TABLE.InWardDetail.WAREHOUSE_ACCOUNT['vmodel']
								]
							"
							:value="
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
							v-model="data['dataMaster']['debit_amount']"
							:value="data['dataMaster']['debit_amount']"
							format="money"
						/>
					</div>
					<div class="fx">
						<base-combobox-advance
							label="TK doanh thu"
							class="pr-26 mt-16"
							width="150px"
							type="small"
							:controller="
								WAREHOUSE_TABLE.InWardDetail.DEBIT_ACCOUNT['controller']
							"
							v-model="
								data['dataMaster'][
									WAREHOUSE_TABLE.InWardDetail.DEBIT_ACCOUNT['vmodel']
								]
							"
							:value="
								data['dataMaster'][
									WAREHOUSE_TABLE.InWardDetail.DEBIT_ACCOUNT['vmodel']
								]
							"
							:vmodelField="
								WAREHOUSE_TABLE.InWardDetail.DEBIT_ACCOUNT['vmodel']
							"
							:field="WAREHOUSE_TABLE.InWardDetail.DEBIT_ACCOUNT['field']"
							:default="
								data['dataMaster'][
									WAREHOUSE_TABLE.InWardDetail.DEBIT_ACCOUNT['field']
								]
							"
							:subfield="
								WAREHOUSE_TABLE.InWardDetail.DEBIT_ACCOUNT['subfield']
							"
							:display="WAREHOUSE_TABLE.InWardDetail.DEBIT_ACCOUNT['display']"
							:listGridStyle="
								WAREHOUSE_TABLE.InWardDetail.DEBIT_ACCOUNT['style']
							"
							:form="WAREHOUSE_TABLE.InWardDetail.DEBIT_ACCOUNT['form']"
						/>
						<base-combobox-advance
							label="TK chi phí"
							class="pr-26 mt-16"
							width="150px"
							type="small"
							:controller="
								WAREHOUSE_TABLE.InWardDetail.CREDIT_ACCOUNT['controller']
							"
							v-model="
								data['dataMaster'][
									WAREHOUSE_TABLE.InWardDetail.CREDIT_ACCOUNT['vmodel']
								]
							"
							:value="
								data['dataMaster'][
									WAREHOUSE_TABLE.InWardDetail.CREDIT_ACCOUNT['vmodel']
								]
							"
							:vmodelField="WAREHOUSE_TABLE.InWardDetail.CREDIT_ACCOUNT['vmodel']"
							:field="WAREHOUSE_TABLE.InWardDetail.CREDIT_ACCOUNT['field']"
							:default="
								data['dataMaster'][
									WAREHOUSE_TABLE.InWardDetail.CREDIT_ACCOUNT['field']
								]
							"
							:subfield="WAREHOUSE_TABLE.InWardDetail.CREDIT_ACCOUNT['subfield']"
							:display="WAREHOUSE_TABLE.InWardDetail.CREDIT_ACCOUNT['display']"
							:listGridStyle="
								WAREHOUSE_TABLE.InWardDetail.CREDIT_ACCOUNT['style']
							"
							:form="WAREHOUSE_TABLE.InWardDetail.CREDIT_ACCOUNT['form']"
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
					:escapeValue="data['dataMaster']['unit_id']"
					v-model="data['dataDetail']"	
					:formName="formName"
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
			currIdx: {
				type: Number,
				default: -1,
			},
			formName: {
				type: String,
				default: ''
			}
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
	};
</script>
<style></style>

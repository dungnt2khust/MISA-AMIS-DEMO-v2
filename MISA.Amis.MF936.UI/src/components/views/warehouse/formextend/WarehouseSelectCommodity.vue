<template lang="">
	<base-form-extend v-if="formState" v-model="formState">
		<template v-slot:header>
			<div v-if="currIdx == -1" class="formextend__title">
				Chọn tính chất hàng hoá dịch vụ
			</div>
			<div v-if="currIdx != -1" class="selectcommodity__header">
				<div class="selectcommodity__title">
					Thông tin vật tư, hàng hoá, dịch vụ
				</div>
				<div class="selectcommodity__curr">
					<div
						class="selectcommodity__curr-icon"
						:style="{
							'background-position':
								currIdx != -1 ? data[currIdx]['posSmall'] : '',
						}"
					></div>
					<div class="selectcommondity__curr-title ml-8">
						{{ currIdx != -1 ? data[currIdx]["title"] : "" }}
					</div>
					<div
						@click="currIdx = -1"
						class="selectcommodity__back text-blue ml-8"
					>
						Thay đổi tính chất
					</div>
				</div>
			</div>
		</template>
		<template v-slot:body>
			<div class="selectcommodity">
				<ul v-show="currIdx == -1" class="selectcommodity__list">
					<li
						v-for="(item, index) in data"
						class="selectcommodity__item"
						@click="currIdx = index"
						:key="index"
					>
						<div
							class="selectcommodity__item-icon"
							:style="{ 'background-position': item['pos'] }"
						></div>
						<div class="selectcommodity__item-content">
							<div class="selectcommodity__item-title">{{ item["title"] }}</div>
							<div class="selectcommodity__item-desc">{{ item["desc"] }}</div>
						</div>
					</li>
				</ul>
				<div class="selectcommodity__main" v-if="currIdx != -1">
					<warehouse-commodity
						:data="dataConversionUnit"
						v-model="dataConversionUnit"
						:currIdx="currIdx"
					/>
				</div>
			</div>
		</template>
		<template v-if="currIdx != -1" v-slot:footer>
			<div class="fx-space-between">
				<base-button :method="hideForm" label="Huỷ" />
				<div class="fx">
					<base-button :method="store" label="Cất" class="mr-10" />
					<base-button :method="storeAndAdd" label="Cất và Thêm" type="green" />
				</div>
			</div>
		</template>
	</base-form-extend>
</template>
<script>
	// LIBRARY
	import commodityAPI from "../../../../js/components/commodityAPI"
	import globalComponents from "../../../../mixins/globalComponents/globalComponents.js"
	import methods from "../../../../mixins/methods"
	// COMPONENTS
	import BaseFormExtend from "../../../Base/Form/BaseFormExtend.vue";
	import WarehouseCommodity from "./WarehouseCommodity.vue";
	import BaseButton from "../../../Base/Button/BaseButton.vue";

	export default {
		name: "WarehouseSelectCommodity",
		mixins: [methods, globalComponents],
		components: {
			BaseFormExtend,
			WarehouseCommodity,
			BaseButton,
		},
		data() {
			return {
				dataConversionUnit: {
					dataMaster: {}, 
					dataDetail: []
				},
				dataClone: {},
				fullscreen: false,
				currIdx: -1,
				formState: false,
				data: [
					{
						pos: "-32px -632px",
						posSmall: "-32px -748px",
						title: "Hàng hoá",
						desc: "Sản phẩm bạn mua và bán lại cho khách hàng",
					},
					{
						pos: "-264px -632px",
						posSmall: "-170px -748px",
						title: "Dịch vụ",
						desc: "Dịch vụ mà bạn cung cấp cho khách hàng",
					},
					{
						pos: "-960px -632px",
						posSmall: "-597px -748px",
						title: "Nguyên vật liệu",
						desc:
							"Nguyên liệu đầu vào dùng cho hoạt động sản xuất, xây dựng, cung cấp dịch vụ",
					},
					{
						pos: "-148px -632px",
						posSmall: "-101px -748px",
						title: "Thành phẩm",
						desc: "Là sản phẩm đầu ra của quá trình sản xuất",
					},
					{
						pos: "-1079px -632px",
						posSmall: "-678px -749px",
						title: "Công cụ dụng cụ",
						desc: "Công cụ dụng cụ mua về nhập kho chưa đưa vào sử dụng",
					},
				],
			};
		},
		created() {
			this.$bus.$on("showWarehouseAddCommodity", () => {
				this.formState = true;
				this.currIdx = -1;
			});
		},
		methods: {
			/**
			 * Tắt form
			 * CreatedBy: NTDUNG (01/10/2021)
			 */
			hideForm() {
				if (this.deepEqualObject(this.dataConversionUnit, this.dataClone))
					this.formState = false;
				else
					this.callDialog(
						this.$enum.DIALOG_TYPE.ConfirmCancel,
						this.$resourcesVN.NOTIFY.DataHasChanged
					).then((answer) => {
						switch (answer) {
							case this.$enum.DIALOG_RESULT.Yes:
								this.store();
								break;
							case this.$enum.DIALOG_RESULT.No:
								this.formState = false;
								break;
							case this.$enum.DIALOG_RESULT.Cancel:
								break;
						}
					});
			},
			/**
			 * Cất
			 * CreatedBy: NTDUNG (03/10/2021)
			 */
			store() {
				console.log(this.dataConversionUnit);
				if (!this.deepEqualObject(this.dataConversionUnit, this.dataClone)) {
					commodityAPI
						.AddCommodity(this.dataConversionUnit)
						.then((res) => {
							console.log(res);
						})
						.catch((res) => {
							console.log(res);
						});
					this.formState = false;
				} else this.formState = false;
			},
			/**
			 * Cất và thêm
			 * CreatedBy: NTDUNG (03/10/2021)
			 */
			storeAndAdd() {
				console.log(this.data);
			},
			/**
			 * Clone data
			 * CreatedBy: NTDUNG (03/10/2021)
			 */
			cloneData() {
				this.dataClone = { 
					dataMaster: {...this.dataConversionUnit },
					dataDetail: []
				},
				this.dataConversionUnit['dataDetail'].forEach((item) => {
					this.dataClone['dataDetail'].push(item);
				});
			},
		},
		watch: {
			currIdx: function(value) {
				if (value != -1) {
					this.dataConversionUnit = {
						dataMaster: {},
						dataDetail: []
					};
					setTimeout(() => {this.cloneData()}, 1000);
				}
			}
		}
	};
</script>
<style>
	@import url("../../../../css/views/warehouse/warehouseselectcommodity.css");
</style>

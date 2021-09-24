<template lang="">
	<base-form-extend @hideForm="hideForm()">
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
				<div v-if="currIdx != -1">
					<warehouse-commodity v-if="currIdx == 0"/>
					<warehouse-service v-if="currIdx == 1"/>
					<warehouse-matarial v-if="currIdx == 2"/>
					<warehouse-product v-if="currIdx == 3"/>
					<warehouse-tool v-if="currIdx == 4"/>
				</div>
			</div>
		</template>
	</base-form-extend>
</template>
<script>
	// COMPONENTS
	import BaseFormExtend from "../../../Base/Form/BaseFormExtend.vue";
	import WarehouseCommodity from "./WarehouseCommodity.vue"
	import WarehouseService from "./WarehouseService.vue"
	import WarehouseMatarial from "./WarehouseMatarial.vue"
	import WarehouseProduct from "./WarehouseProduct.vue"
	import WarehouseTool from "./WarehouseTool.vue"

	export default {
		name: "WarehouseSelectCommodity",
		components: {
			BaseFormExtend,
			WarehouseCommodity,
			WarehouseService,
			WarehouseMatarial,
			WarehouseProduct,
			WarehouseTool
		},
		data() {
			return {
				fullscreen: false,
				currIdx: -1,
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
		methods: {
			hideForm() {
				this.$emit("input", false);
			},
		},
	};
</script>
<style>
	@import url("../../../../css/views/warehouse/warehouseselectcommodity.css");
</style>

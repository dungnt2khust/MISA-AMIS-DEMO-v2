import Vue from "vue";
import VueRouter from "vue-router";
// Employee
import EmployeePage from "../components/views/employee/EmployeePage";
// Customer
import CustomerPage from "../components/views/customer/CustomerPage";
// Warehouse
import WarehousePage from "../components/views/warehouse/WarehousePage";
import WarehouseProcess from "../components/views/warehouse/tabs/WarehouseProcess";
import WarehouseInwardOutwardList from "../components/views/warehouse/tabs/WarehouseInwardOutwardList";
import WarehouseReport from "../components/views/warehouse/tabs/WarehouseReport";


Vue.use(VueRouter);

const routes = [
	{ path: "/", redirect: "/warehouse" },
	{ path: "/employees", component: EmployeePage },
	{ path: "/customers", component: CustomerPage },
	{
		path: "/warehouse",
		redirect: "/warehouse/process",
		component: WarehousePage,
		children: [
			{ path: "/warehouse/process", component: WarehouseProcess },
			{ path: "/warehouse/InwardOutwardList/:mode", component: WarehouseInwardOutwardList },
			{ path: "/warehouse/report", component: WarehouseReport },
		],
	},
];

const router = new VueRouter({
	routes: routes,
	mode: "history",
});

export default router;

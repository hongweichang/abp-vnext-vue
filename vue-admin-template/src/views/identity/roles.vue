<template>
  <div class="app-container">
    <div class="filter-container">
      <el-button
        v-if="checkPermission('AbpIdentity.Roles.Create')"
        class="filter-item"
        style="margin-left: 10px;"
        type="primary"
        icon="el-icon-edit"
        @click="handleCreate"
      >
        新角色
      </el-button>
    </div>
    <el-table
      :key="tableKey"
      v-loading="listLoading"
      :data="list"
      border
      fit
      highlight-current-row
      style="width: 100%;"
      @sort-change="sortChange"
    >
      <el-table-column
        label="角色名称"
        prop="name"
        sortable
        align="center"
      >
        <template slot-scope="{ row }">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="操作"
        align="center"
        width="300"
        class-name="small-padding fixed-width"
      >
        <template slot-scope="{ row, $index }">
          <el-button
            v-if="checkPermission('AbpIdentity.Roles.Update')"
            type="primary"
            size="mini"
            @click="handleUpdate(row)"
          >
            编辑
          </el-button>
          <el-button
            v-if="checkPermission('AbpIdentity.Roles.ManagePermissions')"
            type="primary"
            size="mini"
            @click="handleUpdatePermission(row)"
          >
            权限
          </el-button>
          <el-button
            v-if="!row.isStatic && checkPermission('AbpIdentity.Roles.Delete')"
            size="mini"
            type="danger"
            @click="handleDelete(row, $index)"
          >
            删除
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination
      v-show="total > 0"
      :total="total"
      :page.sync="listQuery.page"
      :limit.sync="listQuery.limit"
      @pagination="getList"
    />

    <el-dialog
      :title="
        dialogStatus == 'create'
          ? '新角色'
          : '编辑'
      "
      :visible.sync="dialogFormVisible"
    >
      <el-form
        ref="dataForm"
        :rules="rules"
        :model="temp"
        label-position="right"
        label-width="120px"
      >
        <el-form-item label="角色名称" prop="name">
          <el-input v-model="temp.name" :disabled="temp.isStatic" />
        </el-form-item>
        <el-form-item>
          <el-checkbox
            v-model="temp.isDefault"
            label="默认"
          />
        </el-form-item>
        <el-form-item>
          <el-checkbox
            v-model="temp.isPublic"
            label="公开"
          />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">
          取消
        </el-button>
        <el-button
          type="primary"
          @click="dialogStatus === 'create' ? createData() : updateData()"
        >
          保存
        </el-button>
      </div>
    </el-dialog>

    <permission-dialog ref="permissionDialog" provider-name="R" />
  </div>
</template>

<script>
import {
  getRoles,
  createRole,
  getRoleById,
  updateRole,
  deleteRole
} from '@/api/identity/role'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import baseListQuery, { checkPermission } from '@/utils/abp'
import PermissionDialog from './components/permission-dialog'

export default {
  name: 'Roles',
  components: { Pagination, PermissionDialog },
  data() {
    return {
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: baseListQuery,
      temp: {
        name: '',
        isDefault: false,
        isPublic: false
      },
      dialogFormVisible: false,
      dialogStatus: '',
      rules: {
        name: [
          {
            required: true,
            message: '角色名称不可为空',
            trigger: 'blur'
          },
          {
            max: 256,
            message: '字段最大长度为256',
            trigger: 'blur'
          }
        ]
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    checkPermission,
    getList() {
      this.listLoading = true
      getRoles(this.listQuery).then(response => {
        this.list = response.items
        this.total = response.totalCount

        this.listLoading = false
      })
    },
    handleFilter(firstPage = true) {
      if (firstPage) this.listQuery.page = 1
      this.getList()
    },
    sortChange(data) {
      const { prop, order } = data
      this.listQuery.sort = order ? `${prop} ${order}` : undefined
      this.handleFilter()
    },
    resetTemp() {
      this.temp = {
        name: '',
        isDefault: false,
        isPublic: false
      }
    },
    handleCreate() {
      this.resetTemp()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    createData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          createRole(this.temp).then(() => {
            this.handleFilter()
            this.dialogFormVisible = false
            this.$notify({
              title: '成功',
              message: '成功',
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },
    handleUpdate(row) {
      this.temp = Object.assign({}, row) // copy obj
      this.dialogStatus = 'update'
      this.dialogFormVisible = true

      getRoleById(row.id).then(response => {
        this.temp = response
      })

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          updateRole(this.temp).then(() => {
            this.handleFilter(false)
            this.dialogFormVisible = false
            this.$notify({
              title: '成功',
              message: '成功',
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },
    handleDelete(row, index) {
      this.$confirm(
        this.$i18n.t("AbpIdentity['RoleDeletionConfirmationMessage']", [
          row.name
        ]),
        this.$i18n.t("AbpIdentity['AreYouSure']"),
        {
          confirmButtonText: '确认',
          cancelButtonText: '取消',
          type: 'warning'
        }
      ).then(async() => {
        deleteRole(row.id).then(() => {
          this.handleFilter()
          this.$notify({
            title: '成功',
            message: '成功',
            type: 'success',
            duration: 2000
          })
        })
      })
    },
    handleUpdatePermission(row) {
      this.$refs['permissionDialog'].handleUpdatePermission(row)
    }
  }
}
</script>

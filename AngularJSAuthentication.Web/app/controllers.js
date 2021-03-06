﻿(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('loginCtrl', loginCtrl);

    loginCtrl.$inject = ['$scope', '$location', 'authService', 'ngAuthSettings', 'utility', 'passwordRecovery'];

    function loginCtrl($scope, $location, authService, ngAuthSettings, utility, passwordRecovery) {

        $scope.loginData = {
            userName: "",
            password: "",
            useRefreshTokens: false
        };

        $scope.message = "";

        $scope.login = function() {
            $scope.inProgress = true;
            authService.login($scope.loginData).then(function() {
                    $location.path('/dashboard');
                },
                function(err) {
                    $scope.message = err.error_description;
                    $scope.inProgress = false;
                });
        };

        $scope.authExternalProvider = function(provider) {

            var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';

            var externalProviderUrl = ngAuthSettings.apiServiceBaseUri + "api/Account/ExternalLogin?provider=" + provider
                + "&response_type=token&client_id=" + ngAuthSettings.clientId
                + "&redirect_uri=" + redirectUri;
            window.$windowScope = $scope;

            window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
        };

        $scope.authCompletedCB = function(fragment) {
            $scope.$apply(function() {
                if (fragment.haslocalaccount == 'False') {
                    authService.logOut();
                    authService.externalAuthData = {
                        provider: fragment.provider,
                        userName: fragment.external_user_name,
                        externalAccessToken: fragment.external_access_token
                    };
                    $location.path('/associate');

                } else {
                    //Obtain access token and redirect to orders
                    var externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
                    authService.obtainAccessToken(externalData).then(function() {
                            $location.path('/dashboard');
                        },
                        function(err) {
                            $scope.message = err.error_description;
                        });
                }

            });
        };

        $scope.forgotPassword = function() {
            utility.confirm("Do you want a new password to be sent to your registered e-mail?")
                .result.then(function() {
                    passwordRecovery.recoverPassword().result.then(function(data) {
                        utility.confirm(data);
                    });
                });
        };
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope', '$location', 'authService'];

    function indexCtrl($scope, $location, authService) {
        $scope.logOut = function() {
            authService.logOut();
            $location.path('/');
        }

        $scope.authentication = authService.authentication;
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('dashboardCtrl', dashboardCtrl);

    function dashboardCtrl() {
        var vm = this;
        vm.name = 'dashboardCtrl';

    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('usermanagementCtrl', usermanagementCtrl);

    usermanagementCtrl.$inject = [
        'userManagementService', 'rolesService', 'userModel', 'authService', 'utility',
        'userManagement', 'accountLocksService'
    ];

    function usermanagementCtrl(userManagementService, rolesService, userModel, authService, utility,
        userManagement, accountLocksService) {
        var vm = this;
        vm.name = 'usermanagementCtrl';

        vm.availableRoles = [];
        vm.newUser = new userModel();
        vm.users = [];

        vm.editUser = function(user) {
            userManagement.editUser(user).result.then(function() {
                vm.getAllUsers();
            });
        }

        vm.createUser = function() {
            utility.confirm("Are you sure you want to create this user?").result.then(function() {
                authService.saveRegistration(vm.newUser).then(function() {
                    //todo consider sending back in payload so we dont have to do another call to refresh the screen
                    vm.getAllUsers();
                    vm.newUser = new userModel();
                });
            });
        }

        vm.deleteUser = function(id) {
            utility.confirm("Are you sure you want to delete this user?").result.then(function() {
                userManagementService.deleteUser(id).then(function() {
                    _.remove(vm.users, { 'id': id });
                });
            });
        }

        vm.init = function() {
            vm.getAllUsers();

            rolesService.getAllRoles().then(function(result) {
                vm.availableRoles = result.data;
            });
        }

        vm.getAllUsers = function() {
            userManagementService.getAllUsers().then(function(result) {
                vm.users = result.data;
            });
        }

        vm.unlock = function(user) {
            utility.confirm("Are you sure you want to unlock this user?").result.then(function() {
                accountLocksService.unlock(user.id).then(function() {
                    user.isLocked = false;
                });
            });
        }

        vm.lock = function(user) {
            utility.confirm("Are you sure you want to lock this user?").result.then(function() {
                accountLocksService.lock(user.id).then(function() {
                    user.isLocked = true;
                });
            });
        }

        vm.init();
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('rolemanagementCtrl', rolemanagementCtrl);

    rolemanagementCtrl.$inject = ['rolesService', 'utility'];

    function rolemanagementCtrl(rolesService, utility) {
        var vm = this;
        vm.roles = [];
        vm.newRole = "";

        vm.deleteRole = function(id) {
            utility.confirm("Are you sure you want to delete this role?").result.then(function() {
                rolesService.deleteRole(id).then(function() {
                    vm.getRoles();
                });
            });
        }

        vm.createRole = function() {
            utility.confirm("Are you sure you want to create this role?").result.then(function() {
                rolesService.createRole(vm.newRole).then(function(result) {
                    vm.newRole = "";
                    vm.roles = result.data;
                });
            });
        }

        vm.getRoles = function() {
            rolesService.getAllRoles().then(function(result) {
                vm.roles = result.data;
            });
        }

        vm.init = function() {
            vm.getRoles();
        }

        vm.init();
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('confirmCtrl', confirmCtrl);

    confirmCtrl.$inject = ['$modalInstance', 'message'];

    function confirmCtrl($modalInstance, message) {
        var vm = this;
        vm.message = message;
        vm.$modalInstance = $modalInstance;
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('editUserCtrl', editUserCtrl);

    editUserCtrl.$inject = ['$modalInstance', 'user', 'userManagementService', 'rolesService'];

    function editUserCtrl($modalInstance, user, userManagementService, rolesService) {
        var vm = this;
        vm.user = user;
        vm.availableRoles = [];
        vm.$modalInstance = $modalInstance;

        vm.updateUser = function() {
            userManagementService.updateUser(vm.user).then(function() {
                $modalInstance.close();
            });
        }

        vm.init = function() {
            rolesService.getAllRoles().then(function(result) {
                vm.availableRoles = result.data;
            });
        }

        vm.init();
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('passwordRecoveryCtrl', passwordRecoveryCtrl);

    passwordRecoveryCtrl.$inject = ['$modalInstance', 'passwordRecoveryService', 'utility', 'messaging', 'vcAppConstants'];

    function passwordRecoveryCtrl($modalInstance, passwordRecoveryService, utility, messaging, vcAppConstants) {
        var vm = this;
        vm.$modalInstance = $modalInstance;
        vm.email = "";

        vm.resetPassword = function() {
            passwordRecoveryService.resetPassword(vm.email).then(function() {
                $modalInstance.close('Temporary password has been sent to ' + vm.email);
            }, function() {
                messaging.publish(vcAppConstants.vcErrorNotificationEvt, { message: 'Failed to reset password' }); //todo add interceptor to catch all errors
            });
        }
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('signupCtrl', signupCtrl);

    signupCtrl.$inject = ['utility', 'authService', '$state'];

    function signupCtrl(utility, authService, $state) {
        var vm = this;
        vm.init = init;
        vm.createUser = createUser;

        function init() {
            vm.newUser = {};
        }

        function createUser() {
            vm.inProgress = true;

            utility.confirm("Are you sure you want to create this user?")
                .result.then(
                    function onConfirm() {
                        authService.saveAnonymousRegistration(vm.newUser)
                            .then(
                                function onSuccess() {
                                    $state.transitionTo('login', {}, { reload: true, inherit: false, notify: true, location: "replace" });
                                },
                                function onError() {
                                    vm.inProgress = false;
                                });
                    },
                    function onReject() {
                        vm.inProgress = false;
                    });
        }

        init();
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('categoryManagementCtrl', categoryManagementCtrl);

    categoryManagementCtrl.$inject = ['lookups', 'lookupsService', 'utility', 'categoryFactory'];

    function categoryManagementCtrl(lookups, lookupsService, utility, categoryFactory) {
        var vm = this;

        vm.init = init;
        vm.createCategory = createCategory;
        vm.populateCategories = populateCategories;
        vm.deleteCategory = deleteCategory;
        vm.addLookupAlias = addLookupAlias;
        vm.addLookupValue = addLookupValue;
        vm.selectCategory = selectCategory;
        vm.deleteAlias = deleteAlias;
        vm.deleteLookupValue = deleteLookupValue;
        vm.editLookupValue = editLookupValue;
        vm.editAlias = editAlias;
        vm.editCategory = editCategory;
        vm.addCategory = addCategory;

        function init() {
            vm.populateCategories();
            vm.newLookupAliasIsActive = true;
            vm.newLookupValueIsActive = true;
        }

        function addCategory() {
            categoryFactory.addCategory().result.then(function() {
                vm.populateCategories();
            });
        }

        function editCategory(category) {
            lookups.editCategory(category);
        }

        function editLookupValue(lookupValue) {
            lookupsService.editLookupValue(lookupValue).error(function() {
                lookupValue.name = lookupValue.origName; //todo move logic to function & update view to use
                lookupValue.active = lookupValue.origActive;
            });
        }

        function deleteLookupValue(id) {
            utility.confirm('Are you sure you want to delete this lookup value?').result.then(function() {
                lookupsService.deleteLookupValue(id).then(function() {
                    if (id == vm.selectedLookupValue.id) {
                        vm.selectedLookupValue = null;
                    }
                    vm.selectedCategory.values = _.remove(vm.selectedCategory.values, function(n) {
                        return n.id != id;
                    });
                });
            });
        }

        function editAlias(alias) {
            lookupsService.editAlias(alias).error(function() {
                alias.name = alias.origName;
                alias.active = alias.origActive;
            });
        }

        function deleteAlias(id) {
            utility.confirm('Are you sure you want to delete this alias?').result.then(function() {
                lookupsService.deleteAlias(id).then(function() {
                    vm.selectedLookupValue.lookupAliases = _.remove(vm.selectedLookupValue.lookupAliases, function(n) {
                        return n.id != id;
                    });
                });
            });
        }

        function selectCategory(category) {
            if (vm.selectedCategory != category) {
                vm.selectedCategory = category;
                vm.selectedLookupValue = null;
            }
        }

        function deleteCategory(id) {
            lookupsService.deleteCategory(id).then(function() {
                if (id == vm.selectedCategory.id) {
                    vm.selectedCategory = null;
                    vm.selectedLookupValue = null;
                }

                vm.categories = _.remove(vm.categories, function(n) {
                    return n.id != id;
                });
            });
        }

        function populateCategories() {
            lookupsService.getCategories().then(function(result) {
                vm.categories = result.data;
            });
        }

        function createCategory() {
            lookupsService.createCategory(vm.newCategory).then(function() {
                vm.newCategory = {};
                vm.populateCategories();
            });
        }

        function addLookupAlias() {
            if (vm.newLookupAlias) {
                var payload = {
                    name: vm.newLookupAlias,
                    active: vm.newLookupAliasIsActive,
                    lookupValueId: vm.selectedLookupValue.id
                };

                lookupsService.addLookupAlias(payload).then(function(result) {
                    vm.selectedLookupValue.lookupAliases.push(result.data);
                    vm.newLookupAlias = '';
                    vm.newLookupAliasIsActive = true;
                });
            }
        }

        function addLookupValue() {
            if (vm.newLookupValue) {
                var payload = {
                    name: vm.newLookupValue,
                    active: vm.newLookupValueIsActive,
                    categoryId: vm.selectedCategory.id,
                }

                lookupsService.addLookupValue(payload).then(function(result) {
                    vm.selectedCategory.values.push(result.data);
                    vm.newLookupValue = '';
                    vm.newLookupValueIsActive = true;
                });
            }
        }

        vm.init();
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('editCategoryCtrl', editCategoryCtrl);

    editCategoryCtrl.$inject = ['lookupsService', 'category', '$modalInstance'];

    function editCategoryCtrl(lookupsService, category, $modalInstance) {
        var vm = this;
        vm.init = init;
        vm.editCategory = editCategory;
        vm.$modalInstance = $modalInstance;
        vm.lookupsService = lookupsService;

        function init() {
            vm.category = category;
        }

        function editCategory() {
            lookupsService.editCategory(vm.category);
            $modalInstance.close();
        }

        init();
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('timelineCtrl', timelineCtrl);

    timelineCtrl.$inject = ['timelineService', 'parentTimeframes'];

    function timelineCtrl(timelineService, parentTimeframes) {
        var vm = this;
        vm.createTimeframe = createTimeframe;
        vm.populateTimeframes = populateTimeframes;
        vm.timeframes = parentTimeframes.data;
        vm.selectTimeframe = selectTimeframe;

        function selectTimeframe(branch) {
            //branch.id
            //fetch assets for timeframe
            //display assets
        }

        function createTimeframe() {
            timelineService.createTimeframe(vm.timeframe).then(function(result) {
                if (angular.isDefined(vm.timeframe.parentTimeFrame) && vm.timeframe.parentTimeFrame != null) {
                    var index = vm.timeframes.indexOfByProperty('id', vm.timeframe.parentTimeFrame.id);

                    vm.timeframes[index].children.push({ label: result.data.name, id: result.data.id });
                } else {
                    vm.timeframes.push(result.data);
                }

                vm.timeframe = {};
            });
        }

        function populateTimeframes() {
            timelineService.getTimeframes().then(function(result) {
                vm.timeframes = result.data;
            });
        }
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('addCategoryCtrl', addCategoryCtrl);

    addCategoryCtrl.$inject = ['$modalInstance', 'lookupsService'];

    function addCategoryCtrl($modalInstance, lookupsService) {
        var vm = this;
        vm.addLookupValue = addLookupValue;
        vm.removeLookupValue = removeLookupValue;
        vm.createCategory = createCategory;
        vm.$modalInstance = $modalInstance;
        vm.init = init;

        function init() {
            vm.newCategory = {};
            vm.newCategory.values = [];
            vm.newLookupValueIsActive = true;
            vm.newLookupValue = '';
        }

        function removeLookupValue(lookupValue) {
            vm.newCategory.values = _.remove(vm.newLookupValues, function(n) {
                return n.name != lookupValue.name;
            });
        }

        function addLookupValue() {
            if (vm.newCategory.values.indexOfByProperty('name', vm.newLookupValue) < 0) {
                vm.newCategory.values.push({ name: vm.newLookupValue, active: vm.newLookupValueIsActive });
                vm.newLookupValue = '';
                vm.newLookupValueIsActive = true;
            }
        }

        function createCategory() {
            lookupsService.createCategory(vm.newCategory).then(function() {
                $modalInstance.close();
            });
        }

        vm.init();
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('templatesCtrl', templatesCtrl);

    templatesCtrl.$inject = ['activityTemplatesService', 'activityTemplates', 'utility'];

    function templatesCtrl(activityTemplatesService, activityTemplates, utility) {
        var vm = this;
        vm.init = init;
        vm.addTemplate = addTemplate;
        vm.selectTemplate = selectTemplate;
        vm.createTemplateTask = createTemplateTask;
        vm.deleteTemplate = deleteTemplate;
        vm.deleteTemplateTask = deleteTemplateTask;
        vm.editTemplateTask = editTemplateTask;
        vm.editTemplate = editTemplate;

        function init() {
            activityTemplatesService.getTemplates().then(function(result) {
                vm.activityTemplates = result.data;
            });
        }

        function deleteTemplate(id) {
            utility.confirm('Are you sure you want to delete this template?').result.then(function() {
                activityTemplatesService.deleteTemplate(id).then(function() {
                    vm.activityTemplates = _.remove(vm.activityTemplates, function(n) {
                        return n.id !== id;
                    });
                    if (vm.selectedTemplate.id === id) {
                        vm.selectedTemplate = null;
                    }
                });
            });
        }

        function deleteTemplateTask(task) {
            utility.confirm('Are you sure you want to delete this template task?').result.then(function() {
                activityTemplatesService.deleteTemplateTask(task.taskId, task.templateId).then(function() {
                    vm.selectedTemplate.templateTasks = _.remove(vm.selectedTemplate.templateTasks, function(n) {
                        return n.id !== task.id;
                    });
                });
            });
        }

        function selectTemplate(template) {
            vm.selectedTemplate = template;
        }

        function addTemplate() {
            activityTemplates.addTemplate().result.then(function(result) {
                vm.activityTemplates.push(result);
            });
        }

        function editTemplate(template) {
            activityTemplates.editTemplate(angular.copy(template)).result.then(function(result) {
                var index = vm.activityTemplates.indexOfByProperty('id', result.id);
                vm.activityTemplates[index] = result;
                if (vm.selectedTemplate.id == result.id) {
                    vm.selectedTemplate = vm.activityTemplates[index];
                }
            });
        }

        function createTemplateTask() {
            var templateTasksCopy = angular.copy(vm.selectedTemplate.templateTasks);
            activityTemplates.addTemplateTask(vm.selectedTemplate.id, templateTasksCopy, getTemplateValues()).result.then(function(data) {
                vm.selectedTemplate.templateTasks.push(data);
            });
        }

        function editTemplateTask(task) {
            var templateTaskCopy = angular.copy(task);
            var templateTasksCopy = angular.copy(vm.selectedTemplate.templateTasks);
            activityTemplates.editTemplateTask(templateTaskCopy, templateTasksCopy, getTemplateValues()).result.then(function(result) {
                var index = vm.selectedTemplate.templateTasks.indexOfByProperty('id', result.id);
                vm.selectedTemplate.templateTasks[index] = result;
            });
        }

        function getTemplateValues() {
            var templateValues = {
                stage: vm.selectedTemplate.stage,
                domain: vm.selectedTemplate.domain,
                environment: vm.selectedTemplate.environment
            };

            return templateValues;
        }

        vm.init();
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('addTemplateCtrl', addTemplateCtrl);

    addTemplateCtrl.$inject = ['$modalInstance', 'lookupsService', 'activityTemplatesService'];

    function addTemplateCtrl($modalInstance, lookupsService, activityTemplatesService) {
        var vm = this;
        vm.$modalInstance = $modalInstance;
        vm.addTemplate = addTemplate;
        vm.init = init;

        function init() {
            lookupsService.getLookupsByCategoryCode('domain').then(function(result) {
                vm.domainLookups = result.data;
            });

            lookupsService.getLookupsByCategoryCode('stage').then(function(result) {
                vm.stageLookups = result.data;
            });

            lookupsService.getLookupsByCategoryCode('environment').then(function(result) {
                vm.environmentLookups = result.data;
            });
        }

        function addTemplate() {
            activityTemplatesService.createTemplate(vm.template).then(function(result) {
                $modalInstance.close(result.data);
            });
        }

        vm.init();
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('addTemplateTaskCtrl', addTemplateTaskCtrl);

    addTemplateTaskCtrl.$inject = [
        '$modalInstance', 'activityTemplatesService', 'templateId', 'templateTasks', 'lookupsService',
        'templateValues'
    ];

    function addTemplateTaskCtrl($modalInstance, activityTemplatesService, templateId, templateTasks, lookupsService,
        templateValues) {
        var vm = this;
        vm.init = init;
        vm.addTemplateTask = addTemplateTask;
        vm.handleNewTaskToggle = handleNewTaskToggle;
        vm.$modalInstance = $modalInstance;
        vm.templateValues = templateValues;

        function init() {
            vm.templateTask = {};
            vm.templateTask.templateId = templateId;
            vm.templateTask.stage = templateValues.stage;
            vm.templateTask.domain = templateValues.domain;
            vm.templateTask.environment = templateValues.environment;

            if (!vm.templateTask.domain) {
                lookupsService.getLookupsByCategoryCode('domain').then(function(result) {
                    vm.domainLookups = result.data;
                });
            }

            if (!vm.templateTask.stage) {
                lookupsService.getLookupsByCategoryCode('stage').then(function(result) {
                    vm.stageLookups = result.data;
                });
            }

            if (!vm.templateTask.environment) {
                lookupsService.getLookupsByCategoryCode('environment').then(function(result) {
                    vm.environmentLookups = result.data;
                });
            }

            activityTemplatesService.getActivityTasks().then(function(result) {
                result.data = _.remove(result.data, function(n) {
                    return !(templateTasks.indexOfByProperty('taskId', n.id) > -1);
                });
                vm.activityTasks = result.data;
            });
        }

        function addTemplateTask() {
            activityTemplatesService.createTemplateTask(vm.templateTask).then(function(result) {
                $modalInstance.close(result.data);
            });
        }

        function handleNewTaskToggle() {
            if (vm.isNewTask) {
                vm.templateTask.taskId = null;
            }
        }

        vm.init();
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('editTemplateTaskCtrl', editTemplateTaskCtrl);

    editTemplateTaskCtrl.$inject = ['templateTask', 'activityTemplatesService', 'templateTasks', 'templateValues',
        'lookupsService', '$q', '$modalInstance'];

    function editTemplateTaskCtrl(templateTask, activityTemplatesService, templateTasks, templateValues,
        lookupsService, $q, $modalInstance) {
        var vm = this;
        vm.init = init;
        vm.$modalInstance = $modalInstance;
        vm.updateTemplateTask = updateTemplateTask;

        function init() {
            vm.templateValues = templateValues;
            vm.isLoading = true;

            $q.all(
            [
                lookupsService.getLookupsByCategoryCode('domain'),
                lookupsService.getLookupsByCategoryCode('stage'),
                lookupsService.getLookupsByCategoryCode('environment'),
                activityTemplatesService.getActivityTasks()
            ]).then(function(result) {
                vm.domainLookups = result[0].data;
                vm.stageLookups = result[1].data;
                vm.environmentLookups = result[2].data;
                onActivityTasksLoaded(result[3]);
                vm.templateTask = templateTask;
                vm.isLoading = false;
            });
        }

        function onActivityTasksLoaded(result) {
            templateTasks = _.remove(templateTasks, function (n) {
                return n.id !== templateTask.taskId;
            });

            result.data = _.remove(result.data, function (n) {
                return !(templateTasks.indexOfByProperty('taskId', n.id) > -1);
            });

            vm.activityTasks = result.data;
        }

        function updateTemplateTask() {
            activityTemplatesService.updateTemplateTask(vm.templateTask).then(function(result) {
                $modalInstance.close(result.data);
            });
        }

        vm.init();
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('editTemplateCtrl', editTemplateCtrl);

    editTemplateCtrl.$inject = ['$q', 'template', 'lookupsService', '$modalInstance', 'activityTemplatesService'];

    function editTemplateCtrl($q, template, lookupsService, $modalInstance, activityTemplatesService) {
        var vm = this;
        vm.init = init;
        vm.$modalInstance = $modalInstance;
        vm.updateTemplate = updateTemplate;

        function init() {
            vm.isLoading = true;
            $q.all(
            [
                lookupsService.getLookupsByCategoryCode('domain'),
                lookupsService.getLookupsByCategoryCode('stage'),
                lookupsService.getLookupsByCategoryCode('environment'),
            ]).then(function(result) {
                vm.domainLookups = result[0].data;
                vm.stageLookups = result[1].data;
                vm.environmentLookups = result[2].data;
                vm.template = template;
                vm.isLoading = false;
            });
        }

        function updateTemplate() {
            activityTemplatesService.updateTemplate(vm.template).then(function(result) {
                $modalInstance.close(result.data);
            });
        }

        vm.init();
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('activitytaskManagementCtrl', activitytaskManagementCtrl);

    function activitytaskManagementCtrl() {
        var vm = this;
        vm.init = init;

        function init() {
            
        }

        vm.init();
    }
}(angular));

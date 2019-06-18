(function (global, factory) {
  if (typeof define === "function" && define.amd) {
    define("/Section/GridMenu", ["exports", "jquery", "Component"], factory);
  } else if (typeof exports !== "undefined") {
    factory(exports, require("jquery"), require("Component"));
  } else {
    var mod = {
      exports: {}
    };
    factory(mod.exports, global.jQuery, global.Component);
    global.SectionGridMenu = mod.exports;
  }
})(this, function (_exports, _jquery, _Component2) {
  "use strict";

  Object.defineProperty(_exports, "__esModule", {
    value: true
  });
  _exports.default = void 0;
  _jquery = babelHelpers.interopRequireDefault(_jquery);
  _Component2 = babelHelpers.interopRequireDefault(_Component2);
  var $BODY = (0, _jquery.default)('body');
  var $HTML = (0, _jquery.default)('html');

  var Scrollable =
  /*#__PURE__*/
  function () {
    function Scrollable($el) {
      babelHelpers.classCallCheck(this, Scrollable);
      this.$el = $el;
      this.api = null;
      this.init();
    }

    babelHelpers.createClass(Scrollable, [{
      key: "init",
      value: function init() {
        this.api = this.$el.asScrollable({
          namespace: 'scrollable',
          skin: 'scrollable-inverse',
          direction: 'vertical',
          contentSelector: '>',
          containerSelector: '>'
        }).data('asScrollable');
      }
    }, {
      key: "update",
      value: function update() {
        if (this.api) {
          this.api.update();
        }
      }
    }, {
      key: "enable",
      value: function enable() {
        if (!this.api) {
          this.init();
        }

        if (this.api) {
          this.api.enable();
        }
      }
    }, {
      key: "disable",
      value: function disable() {
        if (this.api) {
          this.api.disable();
        }
      }
    }]);
    return Scrollable;
  }();

  var GridMenu =
  /*#__PURE__*/
  function (_Component) {
    babelHelpers.inherits(GridMenu, _Component);

    function GridMenu() {
      var _babelHelpers$getProt;

      var _this;

      babelHelpers.classCallCheck(this, GridMenu);

      for (var _len = arguments.length, args = new Array(_len), _key = 0; _key < _len; _key++) {
        args[_key] = arguments[_key];
      }

      _this = babelHelpers.possibleConstructorReturn(this, (_babelHelpers$getProt = babelHelpers.getPrototypeOf(GridMenu)).call.apply(_babelHelpers$getProt, [this].concat(args)));
      _this.isOpened = false;
      _this.scrollable = new Scrollable(_this.$el);
      return _this;
    }

    babelHelpers.createClass(GridMenu, [{
      key: "open",
      value: function open() {
        this.animate(function () {
          this.$el.addClass('active');
          (0, _jquery.default)('[data-toggle="gridmenu"]').addClass('active').attr('aria-expanded', true);
          $BODY.addClass('site-gridmenu-active');
          $HTML.addClass('disable-scrolling');
        }, function () {
          this.scrollable.enable();
        });
        this.isOpened = true;
      }
    }, {
      key: "close",
      value: function close() {
        this.animate(function () {
          this.$el.removeClass('active');
          (0, _jquery.default)('[data-toggle="gridmenu"]').addClass('active').attr('aria-expanded', true);
          $BODY.removeClass('site-gridmenu-active');
          $HTML.removeClass('disable-scrolling');
        }, function () {
          this.scrollable.disable();
        });
        this.isOpened = false;
      }
    }, {
      key: "toggle",
      value: function toggle(opened) {
        if (opened) {
          this.open();
        } else {
          this.close();
        }
      }
    }, {
      key: "animate",
      value: function animate(doing, callback) {
        var _this2 = this;

        doing.call(this);
        this.$el.trigger('changing.site.gridmenu');
        setTimeout(function () {
          callback.call(_this2);

          _this2.$el.trigger('changed.site.gridmenu');
        }, 500);
      }
    }]);
    return GridMenu;
  }(_Component2.default);

  var _default = GridMenu;
  _exports.default = _default;
});
(function (global, factory) {
  if (typeof define === "function" && define.amd) {
    define("/Section/Menubar", ["exports", "jquery", "Component"], factory);
  } else if (typeof exports !== "undefined") {
    factory(exports, require("jquery"), require("Component"));
  } else {
    var mod = {
      exports: {}
    };
    factory(mod.exports, global.jQuery, global.Component);
    global.SectionMenubar = mod.exports;
  }
})(this, function (_exports, _jquery, _Component2) {
  "use strict";

  Object.defineProperty(_exports, "__esModule", {
    value: true
  });
  _exports.default = void 0;
  _jquery = babelHelpers.interopRequireDefault(_jquery);
  _Component2 = babelHelpers.interopRequireDefault(_Component2);
  var $BODY = (0, _jquery.default)('body');
  var $HTML = (0, _jquery.default)('html');

  var Scrollable =
  /*#__PURE__*/
  function () {
    function Scrollable($el) {
      babelHelpers.classCallCheck(this, Scrollable);
      this.$el = $el;
      this.native = false;
      this.api = null;
      this.init();
    }

    babelHelpers.createClass(Scrollable, [{
      key: "init",
      value: function init() {
        if ($BODY.is('.site-menubar-native')) {
          this.native = true;
          return;
        }

        this.api = this.$el.asScrollable({
          namespace: 'scrollable',
          skin: 'scrollable-inverse',
          direction: 'vertical',
          contentSelector: '>',
          containerSelector: '>'
        }).data('asScrollable');
      }
    }, {
      key: "update",
      value: function update() {
        if (this.api) {
          this.api.update();
        }
      }
    }, {
      key: "enable",
      value: function enable() {
        if (this.native) {
          return;
        }

        if (!this.api) {
          this.init();
        }

        if (this.api) {
          this.api.enable();
        }
      }
    }, {
      key: "disable",
      value: function disable() {
        if (this.api) {
          this.api.disable();
        }
      }
    }]);
    return Scrollable;
  }();

  var Hoverscroll =
  /*#__PURE__*/
  function () {
    function Hoverscroll($el) {
      babelHelpers.classCallCheck(this, Hoverscroll);
      this.$el = $el;
      this.api = null;
      this.init();
    }

    babelHelpers.createClass(Hoverscroll, [{
      key: "init",
      value: function init() {
        this.api = this.$el.asHoverScroll({
          namespace: 'hoverscorll',
          direction: 'vertical',
          list: '.site-menu',
          item: '> li',
          exception: '.site-menu-sub',
          fixed: false,
          boundary: 100,
          onEnter: function onEnter() {// $(this).siblings().removeClass('hover'); $(this).addClass('hover');
          },
          onLeave: function onLeave() {// $(this).removeClass('hover');
          }
        }).data('asHoverScroll');
      }
    }, {
      key: "update",
      value: function update() {
        if (this.api) {
          this.api.update();
        }
      }
    }, {
      key: "enable",
      value: function enable() {
        if (!this.api) {
          this.init();
        }

        if (this.api) {
          this.api.enable();
        }
      }
    }, {
      key: "disable",
      value: function disable() {
        if (this.api) {
          this.api.disable();
        }
      }
    }]);
    return Hoverscroll;
  }();

  var Menubar =
  /*#__PURE__*/
  function (_Component) {
    babelHelpers.inherits(Menubar, _Component);

    function Menubar() {
      var _babelHelpers$getProt;

      var _this;

      babelHelpers.classCallCheck(this, Menubar);

      for (var _len = arguments.length, args = new Array(_len), _key = 0; _key < _len; _key++) {
        args[_key] = arguments[_key];
      }

      _this = babelHelpers.possibleConstructorReturn(this, (_babelHelpers$getProt = babelHelpers.getPrototypeOf(Menubar)).call.apply(_babelHelpers$getProt, [this].concat(args)));
      _this.top = false;
      _this.folded = false;
      _this.foldAlt = false;
      _this.$menuBody = _this.$el.children('.site-menubar-body');
      _this.$menu = _this.$el.find('[data-plugin=menu]');

      if ($BODY.data('autoMenubar') === false || $BODY.is('.site-menubar-keep')) {
        if ($BODY.hasClass('site-menubar-fold')) {
          _this.auto = 'fold';
        } else if ($BODY.hasClass('site-menubar-unfold')) {
          _this.auto = 'unfold';
        }
      } else {
        _this.auto = true;
      }

      var breakpoint = Breakpoints.current();

      if (_this.auto === true) {
        if (breakpoint) {
          switch (breakpoint.name) {
            case 'lg':
              _this.type = 'unfold';
              break;

            case 'md':
            case 'sm':
              _this.type = 'fold';
              break;

            case 'xs':
              _this.type = 'hide';
              break;
          }
        }
      } else {
        switch (_this.auto) {
          case 'fold':
            if (breakpoint.name === 'xs') {
              _this.type = 'hide';
            } else {
              _this.type = 'fold';
            }

            break;

          case 'unfold':
            if (breakpoint.name === 'xs') {
              _this.type = 'hide';
            } else {
              _this.type = 'unfold';
            }

            break;
        }
      }

      return _this;
    }

    babelHelpers.createClass(Menubar, [{
      key: "initialize",
      value: function initialize() {
        if (this.$menuBody.length > 0) {
          this.initialized = true;
        } else {
          this.initialized = false;
          return;
        }

        this.scrollable = new Scrollable(this.$menuBody);
        this.hoverscroll = new Hoverscroll(this.$menuBody);
        $HTML.removeClass('css-menubar').addClass('js-menubar');

        if ($BODY.is('.site-menubar-top')) {
          this.top = true;
        }

        if ($BODY.is('.site-menubar-fold-alt')) {
          this.foldAlt = true;
        }

        this.change(this.type);
      }
    }, {
      key: "process",
      value: function process() {
        (0, _jquery.default)('.site-menu-sub').on('touchstart', function (e) {
          e.stopPropagation();
        }).on('ponitstart', function (e) {
          e.stopPropagation();
        });
      }
    }, {
      key: "getMenuApi",
      value: function getMenuApi() {
        return this.$menu.data('menuApi');
      }
    }, {
      key: "setMenuData",
      value: function setMenuData() {
        var api = this.getMenuApi();

        if (api) {
          api.folded = this.folded;
          api.foldAlt = this.foldAlt;
          api.outerHeight = this.$el.outerHeight();
        }
      }
    }, {
      key: "update",
      value: function update() {
        this.scrollable.update();
        this.hoverscroll.update();
      }
    }, {
      key: "change",
      value: function change(type) {
        if (this.initialized) {
          this.reset();
          this[type]();
          this.setMenuData();
        }
      }
    }, {
      key: "animate",
      value: function animate(doing) {
        var _this2 = this;

        var callback = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : function () {};
        $BODY.addClass('site-menubar-changing');
        doing.call(this);
        this.$el.trigger('changing.site.menubar');
        var menuApi = this.getMenuApi();

        if (menuApi) {
          menuApi.refresh();
        }

        setTimeout(function () {
          callback.call(_this2);
          $BODY.removeClass('site-menubar-changing');

          _this2.update();

          _this2.$el.trigger('changed.site.menubar');
        }, 500);
      }
    }, {
      key: "reset",
      value: function reset() {
        $BODY.removeClass('site-menubar-hide site-menubar-open site-menubar-fold site-menubar-unfold');
        $HTML.removeClass('disable-scrolling');
      }
    }, {
      key: "open",
      value: function open() {
        this.animate(function () {
          $BODY.addClass('site-menubar-open site-menubar-unfold');
          $HTML.addClass('disable-scrolling');
        }, function () {
          this.scrollable.enable();
        });
        this.type = 'open';
      }
    }, {
      key: "hide",
      value: function hide() {
        this.hoverscroll.disable();
        this.animate(function () {
          $BODY.addClass('site-menubar-hide site-menubar-unfold');
        }, function () {
          this.scrollable.enable();
        });
        this.type = 'hide';
      }
    }, {
      key: "unfold",
      value: function unfold() {
        this.hoverscroll.disable();
        this.animate(function () {
          $BODY.addClass('site-menubar-unfold');
          this.folded = false;
        }, function () {
          this.scrollable.enable();
          this.triggerResize();
        });
        this.type = 'unfold';
      }
    }, {
      key: "fold",
      value: function fold() {
        this.scrollable.disable();
        this.animate(function () {
          $BODY.addClass('site-menubar-fold');
          this.folded = true;
        }, function () {
          this.hoverscroll.enable();
          this.triggerResize();
        });
        this.type = 'fold';
      }
    }]);
    return Menubar;
  }(_Component2.default);

  var _default = Menubar;
  _exports.default = _default;
});
(function (global, factory) {
  if (typeof define === "function" && define.amd) {
    define("/Section/PageAside", ["exports", "jquery", "Component"], factory);
  } else if (typeof exports !== "undefined") {
    factory(exports, require("jquery"), require("Component"));
  } else {
    var mod = {
      exports: {}
    };
    factory(mod.exports, global.jQuery, global.Component);
    global.SectionPageAside = mod.exports;
  }
})(this, function (_exports, _jquery, _Component2) {
  "use strict";

  Object.defineProperty(_exports, "__esModule", {
    value: true
  });
  _exports.default = void 0;
  _jquery = babelHelpers.interopRequireDefault(_jquery);
  _Component2 = babelHelpers.interopRequireDefault(_Component2);
  var $BODY = (0, _jquery.default)('body');

  var PageAside =
  /*#__PURE__*/
  function (_Component) {
    babelHelpers.inherits(PageAside, _Component);

    function PageAside() {
      var _babelHelpers$getProt;

      var _this;

      babelHelpers.classCallCheck(this, PageAside);

      for (var _len = arguments.length, args = new Array(_len), _key = 0; _key < _len; _key++) {
        args[_key] = arguments[_key];
      }

      _this = babelHelpers.possibleConstructorReturn(this, (_babelHelpers$getProt = babelHelpers.getPrototypeOf(PageAside)).call.apply(_babelHelpers$getProt, [this].concat(args)));
      _this.$scroll = _this.$el.find('.page-aside-scroll');
      _this.scrollable = _this.$scroll.asScrollable({
        namespace: 'scrollable',
        contentSelector: '> [data-role=\'content\']',
        containerSelector: '> [data-role=\'container\']'
      }).data('asScrollable');
      return _this;
    }

    babelHelpers.createClass(PageAside, [{
      key: "process",
      value: function process() {
        var _this2 = this;

        if ($BODY.is('.page-aside-fixed') || $BODY.is('.page-aside-scroll')) {
          this.$el.on('transitionend', function () {
            _this2.scrollable.update();
          });
        }

        Breakpoints.on('change', function () {
          var current = Breakpoints.current().name;

          if (!$BODY.is('.page-aside-fixed') && !$BODY.is('.page-aside-scroll')) {
            if (current === 'xs') {
              _this2.scrollable.enable();

              _this2.$el.on('transitionend', function () {
                _this2.scrollable.update();
              });
            } else {
              _this2.$el.off('transitionend');

              _this2.scrollable.update();
            }
          }
        });
        (0, _jquery.default)(document).on('click.pageAsideScroll', '.page-aside-switch', function () {
          var isOpen = _this2.$el.hasClass('open');

          if (isOpen) {
            _this2.$el.removeClass('open');
          } else {
            _this2.scrollable.update();

            _this2.$el.addClass('open');
          }
        });
        (0, _jquery.default)(document).on('click.pageAsideScroll', '[data-toggle="collapse"]', function (e) {
          var $trigger = (0, _jquery.default)(e.target);

          if (!$trigger.is('[data-toggle="collapse"]')) {
            $trigger = $trigger.parents('[data-toggle="collapse"]');
          }

          var href;
          var target = $trigger.attr('data-target') || (href = $trigger.attr('href')) && href.replace(/.*(?=#[^\s]+$)/, '');
          var $target = (0, _jquery.default)(target);

          if ($target.attr('id') === 'site-navbar-collapse') {
            _this2.scrollable.update();
          }
        });
      }
    }]);
    return PageAside;
  }(_Component2.default);

  var _default = PageAside;
  _exports.default = _default;
});
(function (global, factory) {
  if (typeof define === "function" && define.amd) {
    define("/Section/Sidebar", ["exports", "jquery", "Base", "Plugin"], factory);
  } else if (typeof exports !== "undefined") {
    factory(exports, require("jquery"), require("Base"), require("Plugin"));
  } else {
    var mod = {
      exports: {}
    };
    factory(mod.exports, global.jQuery, global.Base, global.Plugin);
    global.SectionSidebar = mod.exports;
  }
})(this, function (_exports, _jquery, _Base2, _Plugin) {
  "use strict";

  Object.defineProperty(_exports, "__esModule", {
    value: true
  });
  _exports.default = void 0;
  _jquery = babelHelpers.interopRequireDefault(_jquery);
  _Base2 = babelHelpers.interopRequireDefault(_Base2);

  var Sidebar =
  /*#__PURE__*/
  function (_Base) {
    babelHelpers.inherits(Sidebar, _Base);

    function Sidebar() {
      babelHelpers.classCallCheck(this, Sidebar);
      return babelHelpers.possibleConstructorReturn(this, babelHelpers.getPrototypeOf(Sidebar).apply(this, arguments));
    }

    babelHelpers.createClass(Sidebar, [{
      key: "process",
      value: function process() {
        if (typeof _jquery.default.slidePanel === 'undefined') {
          return;
        }

        var sidebar = this;
        (0, _jquery.default)(document).on('click', '[data-toggle="site-sidebar"]', function () {
          var $this = (0, _jquery.default)(this);
          var direction = 'right';

          if ((0, _jquery.default)('body').hasClass('site-menubar-flipped')) {
            direction = 'left';
          }

          var options = _jquery.default.extend({}, (0, _Plugin.getDefaults)('slidePanel'), {
            direction: direction,
            skin: 'site-sidebar',
            dragTolerance: 80,
            template: function template(options) {
              return "<div class=\"".concat(options.classes.base, " ").concat(options.classes.base, "-").concat(options.direction, "\">\n      <div class=\"").concat(options.classes.content, " site-sidebar-content\"></div>\n      <div class=\"slidePanel-handler\"></div>\n      </div>");
            },
            afterLoad: function afterLoad() {
              var self = this;
              this.$panel.find('.tab-pane').asScrollable({
                namespace: 'scrollable',
                contentSelector: '> div',
                containerSelector: '> div'
              });
              sidebar.initializePlugins(self.$panel);
              this.$panel.on('shown.bs.tab', function () {
                self.$panel.find('.tab-pane.active').asScrollable('update');
              });
            },
            beforeShow: function beforeShow() {
              if (!$this.hasClass('active')) {
                $this.addClass('active');
              }
            },
            afterHide: function afterHide() {
              if ($this.hasClass('active')) {
                $this.removeClass('active');
              }
            }
          });

          if ($this.hasClass('active')) {
            _jquery.default.slidePanel.hide();
          } else {
            var url = $this.data('url');

            if (!url) {
              url = $this.attr('href');
              url = url && url.replace(/.*(?=#[^\s]*$)/, '');
            }

            _jquery.default.slidePanel.show({
              url: url
            }, options);
          }
        });
        (0, _jquery.default)(document).on('click', '[data-toggle="show-chat"]', function () {
          (0, _jquery.default)('#conversation').addClass('active');
        });
        (0, _jquery.default)(document).on('click', '[data-toggle="close-chat"]', function () {
          (0, _jquery.default)('#conversation').removeClass('active');
        });
      }
    }]);
    return Sidebar;
  }(_Base2.default);

  var _default = Sidebar;
  _exports.default = _default;
});
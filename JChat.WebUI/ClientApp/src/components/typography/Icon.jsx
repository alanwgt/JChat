import React from 'react';
import Type from 'prop-types';

const Icon = ({ name, size, effect, renderAs: HtmlTag, ...props }) => {
  let className = `${props.className || ''} lni lni-${name} size-${size}`;

  if (effect) {
    className = `${className} lni-${effect}-effect`;
  }

  return <HtmlTag className={className} {...props} />;
};

const renderAsPropType = Type.oneOfType([
  Type.func,
  Type.string,
  Type.shape({ $$typeof: Type.symbol, render: Type.func }),
  Type.arrayOf(
    Type.oneOfType([
      Type.func,
      Type.string,
      Type.shape({ $$typeof: Type.symbol, render: Type.func }),
    ])
  ),
]);

Icon.propTypes = {
  renderAs: renderAsPropType,
  name: Type.string.isRequired,
  size: Type.oneOf(['lg', 'md', 'sm', 'xs']),
  effect: Type.oneOf([
    'spin',
    'tada',
    'flashing',
    'burst',
    'fade-left',
    'fade-right',
    'fade-up',
    'fade-down',
  ]),
};

Icon.defaultProps = {
  renderAs: 'i',
  size: 'md',
  effect: null,
};

export default Icon;
